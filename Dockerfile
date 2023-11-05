FROM mcr.microsoft.com/dotnet/sdk:7.0

RUN apt update && apt install -y --no-install-recommends \
                                  git

RUN useradd -ms /bin/bash dotnet

USER dotnet

WORKDIR /home/dotnet/app

RUN echo 'HISTFILE=//home/dotnet/zsh/.zsh_history' >> ~/.bashrc

RUN echo 'eval "$(pdm --pep582)"' >> ~/.bashrc && dotnet tool install --global dotnet-ef 
ENV PATH $PATH:/home/dotnet/.dotnet/tools/

CMD ["tail", "-f", "/dev/null"]