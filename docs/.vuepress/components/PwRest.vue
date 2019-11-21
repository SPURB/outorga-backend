<template>
    <div class='pw-rest'>
        <p>{{ method }}</p>
        <p>path<br>{{ path }}</p>
        <p>url<br>{{ url }}</p>
    </div>
</template>

<script>
export default {
    name: 'PwRest',
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