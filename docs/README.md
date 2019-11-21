# API das outorgas
Documentação da API REST de cadastro e acompanhamento das outorgas onerosas das Operações Urbanas Consorciadas gerenciados pela São Paulo Urbanismo.

## Características
 - REST
 - Apenas leitura

### Endpoints

### [/fila](https://servicos.spurbanismo.sp.gov.br/cepacs/api/fila)
Retorna uma lista global de interessados dividos nas seguintes categorias: __Checklist__, __Em análise__, __Indeferido__ e __Aprovado__.

<PwRest
    :path="'fila'"
    :url="'https://servicos.spurbanismo.sp.gov.br/cepacs/api/'"
    :requestType="'Fetch'"
    />


<!-- const xhr = new XMLHttpRequest()
xhr.open('GET', 'http://spurbsp198/estagiario/apiestagio.php/user', true, null, null)
xhr.withCredentials = true
xhr.send() -->