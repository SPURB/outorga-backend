# API das outorgas
Documentação da API REST de cadastro e acompanhamento das outorgas onerosas das Operações Urbanas Consorciadas gerenciados pela São Paulo Urbanismo.

## Características
 - REST
 - Apenas leitura

## Endpoints

<PwRest
    :description="'Retorna objeto de id 914'"
    :path="'fila/914'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
    :method="'GET'"
/>

<PwRest
    :description="'Retorna uma coleção de interessados.'"
    :path="'fila'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
/>
