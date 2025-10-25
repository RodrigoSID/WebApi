1. POST (Criação de Usuário)

Esta rota envia dados no corpo da requisição para criar um novo registro no Supabase.
-Detalhe-----valor
-Método------POST

URL	https://localhost:[PORTA]/api/Users
Corpo (Body)	JSON
Controller	PostUser

{
    "name": "Alexandre Silva",
    "bio": "Estudante de C# e Python",
    "telefone": "77912345678"
}

    Resposta Esperada: 201 Created e a mensagem "Usuario Criado com sucesso".

2. GET (Busca por ID) <- sem um id ele busca todos os Usuarios

Esta rota busca um único usuário usando o ID que você obteve no passo anterior.
-Detalhe----Valor
-Método-----GET

URL	https://localhost:[PORTA]/api/Users/1 (Substitua 1 pelo ID real)
Controller	GetUser

3. PUT (Atualização de Usuário)

Esta rota atualiza um registro existente, usando o ID na URL e os dados a serem alterados no corpo.
-Detalhe----Valor
-Método-----PUT

URL	https://localhost:[PORTA]/api/Users/2 (ID do usuário a ser alterado)
Corpo (Body)	JSON
Controller	PutUser


{
    "onde": 2, <-esss onde referece onde voce que modificar o valor 
                1 = nome 2 = bio 3 = telefone respectivaments

    "novoValor": "Dev Full-Stack com C#" 
}

    Clique em Send.

    Resposta Esperada: 204 No Content. (O código return NoContent(); não retorna corpo, apenas indica sucesso).

4. DELETE (Deletar Usuário)

Esta rota remove um registro usando o ID na URL.
-Detalhe-----Valor
-Método------DELETE

URL	https://localhost:[PORTA]/api/Users/2 (ID do usuário a ser deletado)
Controller	DeleteUser
