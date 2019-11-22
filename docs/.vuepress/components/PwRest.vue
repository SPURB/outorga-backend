<template>
	<section class='pw-rest'>
		<p class="pw-rest__item"><span>method</span> <code>{{ method }}</code></p>
		<p class="pw-rest__item">path<code>{{ path }}</code></p>
		<p class="pw-rest__item">url: <code>{{ url }}</code></p>
		<div class="pw-rest__item">
			<h4>Exemplo de requisição: </h4>
			<div class="language-sh extra-class"><pre class="language-sh"><code><span class="token function">curl</span>{{ requestExample }}</code></pre></div>
		</div>
		<div v-if="api.message !== ''" class="pw-rest__item">
			<h4>Resposta</h4><span v-if="api.message !== ''" class="badge" :class="{ 'error': api.error }">{{ api.message }}</span>
			<div class="language-json extra-class"><pre class="language-json"><code>{{ stringfy(api.response) }}</code></pre></div>
		</div>
		<button id="send" @click.prevent="send(`${url}${path}`, method)">{{ ui.btnTest }} {{ path }}</button>
	</section>
</template>

<script>
export default {
	name: 'PwRest',
	data() {
		return {
			ui: {
				btnTest: 'Testar'
			},
			api: {
				response: 'Aguardando envio',
				message: '',
				error: false
			}
		}
	},
	computed: {
		requestExample () {
			switch(this.method) {
				case 'GET' : return ` -X GET ${this.url}${this.path}`
				case 'POST': return `
					curl -X POST \
						'${this.url}${this.path}' \
						-H 'Content-Length: 2' \
						-H 'Content-Type: application/json; charset=utf-8' \
						-d '${this.bodyParams}'`
			}
		}
	},
	methods: {
		stringfy(jsonStr) {
			return JSON.parse(JSON.stringify(jsonStr), null,2)
		},
		send (fullUrl, method) {
			const oReq = new XMLHttpRequest()
			this.api.message = 'Enviando solicitação...'

			oReq.addEventListener("load", evt => {
				this.api.response = evt.target.response
				this.api.message = 'Sucesso!'
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
		url: {
			type: String,
			required: true
		},
		path: {
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
			}
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