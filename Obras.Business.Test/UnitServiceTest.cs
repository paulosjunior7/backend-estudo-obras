using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Obras.Business.UnitDomain.Models;
using Obras.Business.UnitDomain.Services;
using Obras.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Obras.Data.Entities;
using Obras.Business.UnitDomain.Enums;
using Obras.Business.SharedDomain.Models;

namespace Obras.Business.Test
{
    [TestClass]
    public class UnitServiceTest
    {
        private ObrasDBContext dbContext;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ObrasDBContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            dbContext = new ObrasDBContext(options);

        }

        [TestMethod]
        public async Task Deve_Gerar_Excecao_Ao_Chamar_Create_Sem_Company_Id()
        {
            // Arrange
            var unity = new Unity
            {
                Description = "Test Unity",
                Multiplier = 1.0,
                Active = true
            };
            var mapperMock = ConfigureMapperMock(unity);
            var unityService = new UnityService(dbContext, mapperMock.Object);

            var unityModel = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity",
                Multiplier = 1.0,
                Active = true
            };

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<Exception>(() => unityService.CreateAsync(unityModel));
            Assert.AreEqual("O campo CompanyId não pode ser nulo.", exception.Message);
        }

        [TestMethod]
        public async Task Deve_Cadastrar_Unidade_Quando_Enviado_dados_valido()
        {
            // Arrange
            var unity = new Unity
            {
                Description = "Test Unity",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };
            var mapperMock = ConfigureMapperMock(unity);
            var unityService = new UnityService(dbContext, mapperMock.Object);

            var unityModel = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };

            // Act and Assert
            var dados = await unityService.CreateAsync(unityModel);
            Assert.AreEqual(dados.Description, unityModel.Description);
            Assert.AreEqual(dados.Multiplier, unityModel.Multiplier);
            Assert.IsNotNull(dados.Id);
        }

        [TestMethod]
        public async Task Deve_Gerar_Excecao_ao_Gravar_Unidade_Duplicada_Na_Mesma_Compania()
        {
            // Arrange
            var unity = new Unity
            {
                Description = "Test Unity 1",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };
            var mapperMock = ConfigureMapperMock(unity);
            var unityService = new UnityService(dbContext, mapperMock.Object);

            var unityModel = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 1",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };

            // Act
            await unityService.CreateAsync(unityModel);

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<Exception>(() => unityService.CreateAsync(unityModel));
            Assert.AreEqual("Já existe uma Unidade com esta descrição e multiplicador", exception.Message);
        }

        [TestMethod]
        public async Task Deve_Gravar_Unidade_Duplicada_Caso_Anterior_tiver_Desativada_Na_Mesma_Compania()
        {
            // Arrange
            var unity1 = new Unity
            {
                Description = "Test Unity 2",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = false
            };
            var mapperMock1 = ConfigureMapperMock(unity1);
            var unityService1 = new UnityService(dbContext, mapperMock1.Object);

            var unityModel1 = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 2",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = false
            };

            // Act
            await unityService1.CreateAsync(unityModel1);

            var unity2 = new Unity
            {
                Description = "Test Unity 2",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };
            var mapperMock2 = ConfigureMapperMock(unity2);
            var unityService2 = new UnityService(dbContext, mapperMock2.Object);

            var unityModel2 = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 2",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };

            // Act and Assert
            var dados = await unityService2.CreateAsync(unityModel2);
            Assert.AreEqual(dados.Description, unityModel2.Description);
            Assert.AreEqual(dados.Multiplier, unityModel2.Multiplier);
            Assert.IsNotNull(dados.Id);
        }

        [TestMethod]
        public async Task Deve_Ser_Possivel_Alterar_Uma_Unidade()
        {
            // Arrange
            var unity1 = new Unity
            {
                Description = "Test Unity 3",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };
            var mapperMock1 = ConfigureMapperMock(unity1);
            var unityService1 = new UnityService(dbContext, mapperMock1.Object);

            var unityModel1 = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 3",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };

            // Act
            var retorno = await unityService1.CreateAsync(unityModel1);

            var unity2 = new Unity
            {
                Description = "Test Unity 4",
                Multiplier = 2.0,
                CompanyId = 1,
                Active = false
            };
            var mapperMock2 = ConfigureMapperMock(unity2);
            var unityService2 = new UnityService(dbContext, mapperMock2.Object);

            var unityModel2 = new UnityModel
            {
                Description = "Test Unity 4",
                Multiplier = 2.0,
                CompanyId = 1,
                Active = false
            };

            // Act and Assert
            var dados = await unityService2.UpdateAsync(1, retorno.Id, unityModel2);
            Assert.AreEqual(dados.Description, unityModel2.Description);
            Assert.AreEqual(dados.Multiplier, unityModel2.Multiplier);
            Assert.AreEqual(dados.Active, unityModel2.Active);
            Assert.AreEqual(dados.Id, retorno.Id);
        }

        [TestMethod]
        public async Task Deve_Gerar_Excecao_ao_Alterar_Uma_Unidade_de_compania_diferente()
        {
            // Arrange
            var unity1 = new Unity
            {
                Description = "Test Unity 5",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };
            var mapperMock1 = ConfigureMapperMock(unity1);
            var unityService1 = new UnityService(dbContext, mapperMock1.Object);

            var unityModel1 = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 5",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };

            // Act
            var retorno = await unityService1.CreateAsync(unityModel1);

            var unity2 = new Unity
            {
                Description = "Test Unity 3",
                Multiplier = 2.0,
                CompanyId = 2,
                Active = false
            };
            var mapperMock2 = ConfigureMapperMock(unity2);
            var unityService2 = new UnityService(dbContext, mapperMock2.Object);

            var unityModel2 = new UnityModel
            {
                Description = "Test Unity 3",
                Multiplier = 2.0,
                CompanyId = 2,
                Active = false
            };

            // Act and Assert
            var exception = await Assert.ThrowsExceptionAsync<Exception>(() => unityService2.UpdateAsync(2, retorno.Id, unityModel2));
            Assert.AreEqual("Unidade não encontrada.", exception.Message);
        }

        [TestMethod]
        public async Task Deve_Ser_Possivel_Buscar_Uma_Unidade_Da_Compania()
        {
            // Arrange
            var unity1 = new Unity
            {
                Description = "Test Unity 6",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };
            var mapperMock1 = ConfigureMapperMock(unity1);
            var unityService1 = new UnityService(dbContext, mapperMock1.Object);

            var unityModel1 = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 6",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };

            // Act
            var retorno = await unityService1.CreateAsync(unityModel1);

            var unityRet = await unityService1.GetId(1, retorno.Id);

            // Act and Assert
            Assert.AreEqual(unityRet.Description, unityModel1.Description);
            Assert.AreEqual(unityRet.Multiplier, unityModel1.Multiplier);
            Assert.AreEqual(unityRet.Active, unityModel1.Active);
            Assert.AreEqual(unityRet.Id, retorno.Id);
        }

        [TestMethod]
        public async Task Deve_Retornar_Vazio_AO_Buscar_Uma_Unidade_Que_Nao_Seja_da_Compania()
        {
            // Arrange
            var unity1 = new Unity
            {
                Description = "Test Unity 7",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };
            var mapperMock1 = ConfigureMapperMock(unity1);
            var unityService1 = new UnityService(dbContext, mapperMock1.Object);

            var unityModel1 = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 7",
                Multiplier = 1.0,
                CompanyId = 1,
                Active = true
            };

            // Act
            var retorno = await unityService1.CreateAsync(unityModel1);

            var unityRet = await unityService1.GetId(2, retorno.Id);

            // Act and Assert
            Assert.IsNull(unityRet);
        }

        [TestMethod]
        public async Task Deve_Retornar_Todas_Unidades_Da_Compania()
        {
            // Arrange
            var unity1 = new Unity
            {
                Description = "Test Unity 7",
                Multiplier = 1.0,
                CompanyId = 9999,
                Active = true
            };
            var mapperMock1 = ConfigureMapperMock(unity1);
            var unityService1 = new UnityService(dbContext, mapperMock1.Object);

            var unityModel1 = new UnityModel
            {
                // CompanyId is null here
                Description = "Test Unity 7",
                Multiplier = 1.0,
                CompanyId = 999,
                Active = true
            };

            // Act
            await unityService1.CreateAsync(unityModel1);
            var request = new PageRequest<UnityFilter, UnitySortingFields>()
            {
                Filter = new UnityFilter
                {
                    CompanyId = 999
                }
            };

            var unityRet = await unityService1.GetAsync(request);

            // Act and Assert
            Assert.AreEqual(unityRet.TotalCount, 1);
        }


        private Mock<IMapper> ConfigureMapperMock(Unity unity)
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Unity>(It.IsAny<UnityModel>())).Returns(unity);
            return mapperMock;
        }
    }
}
