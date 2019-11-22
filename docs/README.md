# API das outorgas
Documentação da API REST de cadastro e acompanhamento das outorgas onerosas das Operações Urbanas Consorciadas gerenciados pela São Paulo Urbanismo.

## Características
 - REST
 - Apenas leitura

## Endpoints

## [/fila:id](https://servicos.spurbanismo.sp.gov.br/cepacs/api/fila/917)
Retorna objeto de `:id`.
<PwRest
    :path="'fila/917'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
    :requestType="'cURL'"
    :method="'GET'"
/>

## [/fila](https://servicos.spurbanismo.sp.gov.br/cepacs/api/fila)
Retorna uma coleção de interessados dividos nas seguintes categorias: __Checklist__, __Em análise__, __Indeferido__ e __Aprovado__.
<PwRest
    :path="'fila'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
    :requestType="'cURL'"
    :method="'GET'"
/>
