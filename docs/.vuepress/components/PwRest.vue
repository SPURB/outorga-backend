<template>
	<section class='pw-rest'>
		<h2 class="pw-rest__item">{{ method }} <a :href="url + path">/{{ setPath }}</a></h2>
		<code class="pw-rest__item">{{ url }}{{ setPath }}</code>
		<p class="pw-rest__item description">{{ description }}</p>
		<div class="pw-rest__item example">
			<h4>Exemplo de requisição</h4>
			<h4>{{ setPath }}</h4>

			<select v-model="selectedRequestType">
				<option>JavaScript XHR</option>
				<option>Fetch</option>
				<option>cURL</option>
			</select>

			<div 
				class="extra-class"
				:class="{ 
					'language-sh': selectedRequestType === 'cURL',
					'language-js': selectedRequestType === 'Fetch' || selectedRequestType === 'JavaScript XHR',
				}"
				@click.prevent="copyRequestExample(ui.requestExample)"
				><pre><code>{{ ui.requestExample }}</code></pre></div>
		</div>
		<div v-if="api.message !== ''" class="pw-rest__item">
			<h4>Resposta</h4><span class="badge" :class="{ 'error': api.error }">{{ api.message }}</span>
			<div class="language-json extra-class"><pre class="language-json"><code>{{ stringfyRes(api.response) }}</code></pre></div>
		</div>
		<button id="send" @click.prevent="send(`${url}${path}`, method)">{{ ui.btnTest }} {{ setPath }}</button>
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
				this.api.message = 'Exemplo de resposta'
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
.language-json {
	max-height: 300px;

overflow-x: hidden;
}

</style>