module.exports = {
    base: process.env.CI ? '/outorga-backend/' : '/',
    title: 'Sistema de cadastro de outorga onerosa',
    description: 'Documentação da API REST do cadastro e acompanhamento de outorga onerosa controlados pela São Paulo Urbanismo',
    locales: {
        "/": {
            lang: "pt-br",
            content: "pt-br"
        }
    },
    themeConfig: {
        search: false,
        nav: [
            { text: 'Outorgas da OUCFL', link: 'https://spurb.github.io/relatorios/ouc-faria-lima' },
            { text: 'Github', link: 'https://github.com/spurb/outorga-backend' },
        ],
        sidebar: [
            '/'
        ]
    }
}