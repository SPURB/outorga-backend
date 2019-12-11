# API das outorgas
Documentação da API REST de cadastro e acompanhamento das outorgas onerosas das Operações Urbanas Consorciadas gerenciados pela São Paulo Urbanismo.

## Características
 - REST
 - Apenas leitura

## Fila
<PwRest
    :description="'Coleção de interessados'"
    :path="'fila'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'" 
/>

<PwRest
    :description="'Interessado de /:id'"
    :path="'fila/914'"
    :pathAlias="'fila/:id'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
    :method="'GET'"
/>

## Status
<PwRest
    :description="'Coleção de status'"
    :path="'status'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
/>

<PwRest
    :description="'Nome de status de /:id'"
    :path="'status/2'"
    :pathAlias="'status/:id'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
/>


## Setor Quadras e Lotes
<PwRest
    :description="'Lista de lotes registrados'"
    :path="'sqls'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
/>

<PwRest
    :description="'SQL de /:id'"
    :path="'sqls/3643'"
    :pathAlias="'sqls/:id'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
/>

## Estoque
<PwRest
    :description="'Lista dados gerais de estoques'"
    :path="'estoque'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
/>
