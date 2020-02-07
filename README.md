# API das outorgas [![Build Status](https://travis-ci.org/SPURB/outorga-backend.svg?branch=master)](https://travis-ci.org/SPURB/outorga-backend)

API REST do sistema de moderação do cadastro de interessados de CEPACs - [Certificados de Potencial Adicional de Construção](https://www.prefeitura.sp.gov.br/cidade/secretarias/urbanismo/sp_urbanismo/cepac/index.php?p=19456) - das Operações Urbanas Consorciadas de São Paulo.

Veja a documentação dos dados disponibilizados por esta API:
[spurb.github.io/outorga-backend/](http://spurb.github.io/outorga-backend/)

O objetivo é centralizar o cadastro de todas as manifestações de interesse de CEPACs das Operações Urbanas.

## Características
 - Esta API tem duas endpoints, uma pública e outra privada 
 - A aplicação privada está publicada em https://servicos.spurbanismo.sp.gov.br/cepacs/api/
 - A aplicação privada está publicada na intranet da São Paulo Urbanismo (spurbsp198)
 - Os usuários que realizam a moderação e realizam operações CRUD são controlados pela NTI (Núcleo de Tecnologia da Informação), da São Paulo Urbanismo.

## Instruções para desenvolvimento da API
Ver diretório `FilaCepac/`

## Instruções para desenvolvimento do site de documentação
Utilizamos vuepress para documentação. 
```bash
# Instale as dependências
npm install # ou yarn

# Inicie documentação em localhost:8080
npm run docs:dev
```
Para mais detalhes veja a documentação do [vuepress](https://vuepress.vuejs.org/).
