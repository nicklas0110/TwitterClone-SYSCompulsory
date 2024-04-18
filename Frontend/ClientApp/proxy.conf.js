const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    proxyTimeout: 10000,
    target: 'http://localhost:9090',
    secure: false,
    changeOrigin: true,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
