<template>
	<section class='pw-rest'>
		<h3 class="pw-rest__item">
			<span>{{ method }}</span>
			<a :href="url + path">/{{ setPath }}</a>
		</h3>
		<main class="area request">
			<h4 class="label">URL</h4>
			<code class="pw-rest__item">
				{{ url }}{{ setPath }}
			</code>
			<h4 class="label">Descrição</h4>
			<p class="pw-rest__item description">{{ description }}</p>
			<div class="pw-rest__item example">
				<header>
					<h4 class="label">Exemplo de requisição</h4>
					<div>
						<span>Linguagem:</span>
						<select v-model="selectedRequestType">
							<option>JavaScript XHR</option>
							<option>Fetch</option>
							<option>cURL</option>
						</select>
					</div>
				</header>
				<div 
					class="extra-class"
					:class="{ 
						'language-sh': selectedRequestType === 'cURL',
						'language-js': selectedRequestType === 'Fetch' || selectedRequestType === 'JavaScript XHR',
					}"
					@click.prevent="copyRequestExample(ui.requestExample)"
					><pre><code>{{ ui.requestExample }}</code></pre><span class="onhover">Clique para copiar</span></div>
			</div>
			<button id="send" @click.prevent="send(`${url}${path}`, method)">{{ ui.btnTest }} {{ setPath }}</button>
		</main>
		<aside class="area response">
			<div v-if="api.message !== ''" class="pw-rest__item">
				<h4>Resposta</h4>
				<span class="badge" :class="{ 'error': api.error }">{{ api.message }}</span>
				<div class="language-json extra-class"><pre class="language-json"><code>{{ stringfyRes(api.response) }}</code></pre></div>
			</div>
		</aside>
	</section>
</template>

<script>
export default {
	name: 'PwRest',
	data() {
		return {
			ui: {
				btnTest: 'Testar',
				requestType: 'cURL',
				requestExample: '',
			},
			api: {
				response: 'Enviando',
				message: '',
				error: false
			}
		}
	},
	computed: {
		setPath () { 
			if (this.pathAlias === '') return this.path
			else return this.pathAlias
		},
		selectedRequestType: {
			get () {
				return this.ui.requestType
			},
			set (newReqStr) {
				this.ui.requestType = newReqStr
			}
		}
	},
	watch: {
		selectedRequestType (reqType) {
			this.ui.requestExample = this.getRequestExample(reqType, this.method, `${this.url}${this.path}`)
		}
	},
	created () {
		if (this.requestType !== 'cURL') this.ui.requestType = this.requestType
		this.ui.requestExample = this.getRequestExample(this.requestType, this.method, `${this.url}${this.path}`)
	},
	methods: {
		copyRequestExample(str) {
			console.log(str)
		},
		getRequestExample(reqType, method, reqUrl) {
			if (method === 'GET') {
				switch (reqType) {
					case 'Fetch': return `fetch('${reqUrl}', {
	method: 'GET',
	credentials: 'same-origin'
	}).then(function(response) {
		response.status
		response.statusText
		response.headers
		response.url
		return response.text()
	}).catch(function(error) {
		error.message
})`
					case 'JavaScript XHR': return `const xhr = new XMLHttpRequest()
xhr.open('GET', '${reqUrl}', true, null, null)
xhr.send()`

					default: return `cURL -X GET ${reqUrl}`; break // default is cURL
				}
			}
			else throw new Error (`invalid params reqType:${reqType} or method: ${method} or reqUrl: ${reqUrl}`)
		},
		stringfyRes(jsonStr) {
			return JSON.parse(jsonStr, null,2)
		},
		send (fullUrl, method) {
			this.api.message = 'Enviando solicitação...'

			const oReq = new XMLHttpRequest()
			oReq.addEventListener("load", evt => {
				this.api.response = evt.target.response
				this.api.message = null
				this.ui.btnTest = 'Testar novamente'
			})
			oReq.addEventListener("error", evt => {
				this.api.response = evt.target.response
				this.api.message = 'Erro! A requisição falhou'
			})
			oReq.addEventListener("abort", evt => {
				this.api.message = 'Erro! A requisição foi cancelada'
			})

			oReq.open(method, fullUrl, true, null, null)
			oReq.send()
		}
	},
	props: {
		description: {
			type: String,
			default: ''
		},
		url: {
			type: String,
			required: true
		},
		path: {
			type: String,
			default: ''
		},
		pathAlias: {
			type: String,
			default: ''
		},
		method: {
			type: String,
			default: 'GET'
		},
		label: {
			type: String,
			default: ''
		},
		auth: {
			type: String,
			validator: function (value) {
				return ['None', 'Basic', 'Bearer Token'].indexOf(value) !== -1
			}
		},
		httpUser: {
			type: String,
			default: ''
		},
		httpPassword:{
			type: String,
			default: ''
		},
		bearerToken: {
			type: String,
			default: ''
		},
		headers: {
			type: Array
		},
		params: {
			type: Array
		},
		bodyParams: {
			type: Array
		},
		rawParams: {
			type: String,
			default: ''
		},
		rawInput: {
			type: String,
			default: ''
		},
		contentType: {
			type: String,
			validator: function (value) {
				return [
					'application/json',
					'application/hal+json',
					'application/xml',
					'application/x-www-form-urlencoded',
					'text/html',
					'text/plain'
				].indexOf(value) !== -1
			}
		},
		requestType: {
			type: String,
			validator: function (value) {
				return [
					'JavaScript XHR',
					'Fetch',
					'cURL'
				].indexOf(value) !== -1
			},
			default: 'cURL'
		},
		passwordFieldType: {
			type: String,
			validator: function (value) {
				return [
					'password',
					'text'
				].indexOf(value) !== -1
			}
		}
	}
}
</script>
<style lang='scss' scoped>
.pw-rest {
	box-sizing: border-box;
	width: 100% !important;
	height: calc(100vh - 57px); // header height = 57.6px
	padding: 4.5rem 5rem;
	border-bottom: 1px solid rgba(0, 0, 0, .04);
	&:nth-of-type(2n) { background-color: rgba(0, 0, 0, .02); }
	& > *:first-child { margin-top: 0; }
	& > *:last-child { margin-bottom: 0; }
	display: grid;
	grid: min-content auto / repeat(2, 50%);
	grid-column-gap: 2.5rem;
	& > h3 {
		grid-column: 1 / span 2;
		margin-bottom: 2rem;
		span {
			font-size: 1rem;
			font-weight: normal;
			color: #777;
			background-color: rgba(0, 0, 0, .05);
			padding: 0.25rem 0.5rem;
			border-radius: 0.25rem;
			vertical-align: 0.2rem;
			margin-right: 0.5rem;
		}
	}
	& > .request {
		h4.label {
			margin: 0 0 0.25rem 0;
			font-size: 0.8rem;
			font-weight: normal;
			color: #777;
		}
		& > code {
			display: block;
			white-space: nowrap;
			overflow: hidden;
			text-overflow: ellipsis;
			direction: rtl;
			text-align: left;
			max-width: 100%;
			color: #777;
			margin: 0 0 2rem;
		}
		& > p.description {
			max-width: 100%;
			padding: 0.75rem 1rem;
			background-color: rgba(0, 0, 0, .05);
			border-left: 0.25rem solid #008375;
			border-radius: 0.25rem;
			margin: 0 0 2rem;
		}
		& > .example {
			margin: 4rem 0;
			header {
				display: flex;
				flex-flow: row nowrap;
				align-items: flex-end;
				justify-content: space-between;
				margin: 0 0 0.25rem;
				div {
					span {
						font-size: 0.75rem;
						text-transform: uppercase;
					}
					select {
						font-family: inherit;
						border: 1px solid #EEE;
						border-radius: 2px;
						border-style: solid;
						border-width: 1px;
					}
				}
			}
			.extra-class {
				position: relative;
				margin: 0 0 2rem 0;
				& > pre {
					margin: 0;
					cursor: default;
				}
				& > span.onhover {
					position: absolute;
					top: 0.8em;
					left: 1em;
					font-size: 0.75rem;
					color: rgba(255, 255, 255, .5);
					opacity: 0;
					transition: opacity ease-out .2s .1s;
				}
				&:hover > span.onhover { opacity: 1; }
			}
		}
		button#send {
			width: 100%;
			padding: 1.5rem 2rem;
			background: #008375;
			color: #FFF;
			font-family: inherit;
			font-size: 1rem;
			border: 4px solid rgba(255, 255, 255, .16);
			border-radius: 0.5rem;
			cursor: pointer;
			box-shadow: 0 4px 8px rgba(0, 0, 0, .24);
			transform: translateY(0);
			transition: all ease-in .1s;
			&:hover {
				border-color: rgba(255, 255, 255, .32);
			}
			&:active {
				box-shadow: 0 2px 4px rgba(0, 0, 0, .48);
				transform: translateY(2px);
			}
		}
	}
	& > .response {}
}

</style>