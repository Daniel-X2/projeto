

using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql;


class repository_client: Init_repository
{
    
    
    protected internal static async Task<tipos> Get_client()
    {
        await using var n1=Connect();
        
        await n1.OpenAsync();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM cliente", n1);
       
        tipos lista=new();
        
        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            client campos=new();
            campos.Nome=(string)reader[0];
            campos.cpf=(string)reader[1];
            campos.conta=(int)reader[2];
            campos.isvip=(bool)reader[3];
            lista.lista_client.Add(campos);
        }
         
        return lista;
    }
    protected internal static async Task<int> add_client(string nome,int cpf,int conta,bool isvip)
    {
        int sucesso;
        await using NpgsqlConnection n1 = Connect();
        
        await n1.OpenAsync();

        await using (var cmd = new NpgsqlCommand("INSERT INTO cliente (nome ,cpf, conta,isvip) VALUES (@nome, @cpf, @conta,@isvip)", n1))
        {
            cmd.Parameters.AddWithValue("nome", nome);
            cmd.Parameters.AddWithValue("cpf", cpf);
            cmd.Parameters.AddWithValue("conta", conta);
            cmd.Parameters.AddWithValue("isvip", isvip);
            sucesso=await cmd.ExecuteNonQueryAsync();
        }
        
        return sucesso;
    }  
    
    public static async Task<int> atualizar_client(string antigo_nome,string novo_nome)
    {
        
        await using var n1=Connect();

        await n1.OpenAsync();
        int resultado;
        using (var cmd=new NpgsqlCommand("UPDATE  cliente set nome = @nome WHERE nome =  @antigo_nome", n1))
        {
            cmd.Parameters.AddWithValue("nome",novo_nome);
            cmd.Parameters.AddWithValue("antigo_nome",antigo_nome);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
         return  resultado;

    }
    public static async Task<int> delete(string nome)
    {
        int resultado;
        await using var connect=Connect();

        await connect.OpenAsync();
        //revisar e colocar pra pegar por id
        using (var  cmd = new NpgsqlCommand("DELETE FROM cliente WHERE nome = @nome ", connect))
        {
            cmd.Parameters.AddWithValue("nome",nome);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado ;
        
        
    }
    
}



