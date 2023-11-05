FROM mcr.microsoft.com/dotnet/sdk:7.0

RUN apt update && apt install -y --no-install-recommends \
                                  git

RUN useradd -ms /bin/bash dotnet

USER dotnet

WORKDIR /home/dotnet/app

RUN echo 'HISTFILE=//home/dotnet/zsh/.zsh_history' >> ~/.bashrc

RUN echo 'eval "$(pdm --pep582)"' >> ~/.bashrc

CMD ["tail", "-f", "/dev/null"]