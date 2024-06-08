# Auth Sample Code 

## Criação do Certifcado e chave privada 

```
openssl req -x509 -sha256 -nodes -days 365 -newkey rsa:2048 -keyout privateKey.key -out certificate.crt
```

## Criação de pfx a apartir do certificado e chave privada 

```
openssl pkcs12 -export -out auth.sample.code.pfx -inkey privateKey.key -in certificate.crt
```

## Payload Jwt de Exemplo criado

```json
eyJhbGciOiJSUzI1NiIsImtpZCI6IkExNTYwQjY4Mjk0RjdFOTNGQjU2QzJDNkZGQUUzMDFGQzFEQTk4NjYiLCJ4NXQiOiJvVllMYUNsUGZwUDdWc0xHXzY0d0g4SGFtR1kiLCJ0eXAiOiJKV1QifQ.
eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNzE3ODc3OTIwLCJzY29wZSI6InJlYWQ6bWVzc2FnZXMgd3JpdGU6bWVzc2FnZXMiLCJuYmYiOjE3MTc4Nzc5MjAsImV4cCI6MTcxNzg4MTUxNiwiYXVkIjoiaHR0cHM6Ly9hcGkuZXhhbXBsZS5jb20ifQ.
C-Py_L64Sd-ukd-bluv-tb3T8JfucdJOCcWBJ09WztNRyt0nnlHUNALjIOpNdesV3dmXr1dfG7cf8xWLm0cX0pZb9yhDf3YKo2Bbx6E5bGyQfsLnfJb0muq6yvTLErj_AjrMDIZVRXX4krhiIjHOY07u2N1_CAK3pl2gxhHOML8dpb-DQ13R-37B9ujHBqfBTJJv4-emJviOv4LyL8UZxpg_lNiww1bQv7ulm0G81T6C49LICyV8-fw9JKdru057wPuKVWEWq5CG0N4h08OE00YO0wgMDVsFr2ebbVflLrgSg-sx1mD8cdn197anquyIKRbIVwUoAdfUe3Jd-aAkCw
```

### Header 

```json
{
  "alg": "RS256",
  "kid": "A1560B68294F7E93FB56C2C6FFAE301FC1DA9866",
  "x5t": "oVYLaClPfpP7VsLG_64wH8HamGY",
  "typ": "JWT"
}
```

### Payload Data 

```json
{
  "sub": "1234567890",
  "name": "John Doe",
  "iat": 1717877920,
  "scope": "read:messages write:messages",
  "nbf": 1717877920,
  "exp": 1717881516,
  "aud": "https://api.example.com"
}
```

