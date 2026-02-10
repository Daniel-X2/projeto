//* get_funcionario
//* adicionar_funcionario
//* atualizar_funcionario
//* delete_funcionario
//
using static System.Console;
using Npgsql;



class repository_funcionario:Init_repository
{
    
    private protected static async Task<tipos> Get_funcionario()
    {
        await using var n1=Connect();
        
        await n1.OpenAsync();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM funcionario", n1);
       
        tipos lista=new();

        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            funcionario campos=new();
            campos.nome=(string)reader[0];
            campos.cpf=(string)reader[1];
            campos.isadmin=(bool)reader[2];
            campos.quantidade_atestato=(int)reader[3];
            campos.nascimento=(int)reader[4];
            lista.lista_funcionario.Add(campos);
        }
         
        return lista;
    }
    protected internal static async Task<int> add_funcionario(string nome,int cpf,int quantidade_atestado,bool isadmin,int nascimento)
    {
        int sucesso;
        await using NpgsqlConnection n1 = Connect();
        
        await n1.OpenAsync();

        await using (var cmd = new NpgsqlCommand("INSERT INTO funcionario (nome ,cpf, isadmin,quantidade_atestado,nascimento) VALUES (@nome ,@cpf, @isadmin,@quantidade_atestado,@nascimento)", n1))
        {
            cmd.Parameters.AddWithValue("nome", nome);
            cmd.Parameters.AddWithValue("cpf", cpf);
            cmd.Parameters.AddWithValue("isadmin", isadmin);
            cmd.Parameters.AddWithValue("quantidade_atestado", quantidade_atestado);
            cmd.Parameters.AddWithValue("nascimento",nascimento);
            sucesso=await cmd.ExecuteNonQueryAsync();
        }
        
        return sucesso;
    }  
    
    public static async Task<int> atualizar_client(string antigo_nome,string novo_nome)
    {
        
        await using var n1=Connect();

        await n1.OpenAsync();
        int resultado;
        using (var cmd=new NpgsqlCommand("UPDATE  funcionario set nome = @nome WHERE nome =  @antigo_nome", n1))
        {
            cmd.Parameters.AddWithValue("nome",novo_nome);
            cmd.Parameters.AddWithValue("antigo_nome",antigo_nome);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
         return  resultado;

    }
     protected async Task<int> delete(string nome)
    {
        int sucesso;
        await using var n1=Connect();

        await n1.OpenAsync();
        //revisar e colocar pra pegar por id
        using (var  cmd = new NpgsqlCommand("DELETE FROM funcionario WHERE nome = @nome ", n1))
        {
            cmd.Parameters.AddWithValue("nome",nome);
            sucesso=await cmd.ExecuteNonQueryAsync();
        }
        
        return sucesso ;
        
        
    }


}