module.exports = {
    base: process.env.CI ? '/outorga-backend/' : '/',
    title: 'Sistema de controle e cadastro das OUCs',
    description: 'Documentação da API REST do cadastro e acompanhamento de outorga onerosa controlados pela São Paulo Urbanismo',
    locales: {
        "/": {
            lang: "pt-br",
            content: "pt-br"
        }
    },
    themeConfig: {
        search: false,
    }
}