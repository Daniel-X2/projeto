
using Npgsql;
using Dto;

namespace Api.core.Application.repository
{
    public interface IRepositoryFuncionario
    { 
   internal Task<bool> IsExistsCpf(string cpf);
   internal Task<ListaFuncionario> GetFuncionario();
   internal Task<int> AddFuncionario(FuncionarioDto campos);
   internal Task<int> UpdateFuncionario(FuncionarioDto campos,int id);
   internal Task<int> DeleteFuncionario(int id);
   internal Task<int> GetIdByCpf(string cpf);
    }
class RepositoryFuncionario(IConnect host):IRepositoryFuncionario
{
    public async Task<int> GetIdByCpf(string cpf)
    {
        await using NpgsqlConnection connect = host.Connect();

        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT id FROM funcionario WHERE cpf=@cpf ", connect);
        cmd.Parameters.AddWithValue("cpf", cpf);

        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        return (int)reader["id"];
    }

    public async Task<bool> IsExistsCpf(string cpf)
    {
        await using NpgsqlConnection connect =host.Connect();
        await connect.OpenAsync();
        string sql ="SELECT EXISTS (SELECT 1 FROM funcionario WHERE cpf=@cpf)";
        await using var cmd=new NpgsqlCommand(sql,connect);
        cmd.Parameters.AddWithValue("cpf",cpf);
        bool resultado=(bool) await cmd.ExecuteScalarAsync();
        return resultado;
    }
    public async Task<ListaFuncionario> GetFuncionario()
    {
        
        await using NpgsqlConnection connect=host.Connect();
        
        await connect.OpenAsync();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM funcionario", connect);
       
        ListaFuncionario lista=new();

        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            FuncionarioDto campos=new();
            campos.nome=(string)reader["nome"];
            campos.cpf=(string)reader["cpf"];
            campos.isadmin=(bool)reader["isadmin"];
            campos.quantidade_atestado=(int)reader["quantidade_atestado"];
            campos.nascimento=(int)reader["nascimento"];
            lista.lista_funci.Add(campos);
        }
         
        return lista;
    }
    public  async Task<int> AddFuncionario(FuncionarioDto campos)
    {
        int resultado;
        
        await using NpgsqlConnection connect = host.Connect();
        
        await connect.OpenAsync();

        await using (var cmd = new NpgsqlCommand("INSERT INTO funcionario (nome ,cpf, isadmin,quantidade_atestado,nascimento) VALUES (@nome ,@cpf, @isadmin,@quantidade_atestado,@nascimento)", connect))
        {
            cmd.Parameters.AddWithValue("nome", campos.nome);
            cmd.Parameters.AddWithValue("cpf", campos.cpf);
            cmd.Parameters.AddWithValue("isadmin", campos.isadmin);
            cmd.Parameters.AddWithValue("quantidade_atestado", campos.quantidade_atestado);
            cmd.Parameters.AddWithValue("nascimento",campos.nascimento);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado;
    }  
    
    public  async Task<int> UpdateFuncionario(FuncionarioDto campos,int id)
    {
     
        await using NpgsqlConnection connect=host.Connect();

        await connect.OpenAsync();
        int resultado;
      await  using (var cmd=new NpgsqlCommand("UPDATE  funcionario set nome =@nome,cpf=@cpf,isadmin=@isadmin,quantidade_atestado=@quantidade_atestado,nascimento=@nascimento WHERE id=@id", connect))
        {
            cmd.Parameters.AddWithValue("nome", campos.nome);
            cmd.Parameters.AddWithValue("cpf", campos.cpf);
            cmd.Parameters.AddWithValue("isadmin", campos.isadmin);
            cmd.Parameters.AddWithValue("quantidade_atestado", campos.quantidade_atestado);
            cmd.Parameters.AddWithValue("nascimento",campos.nascimento);
            cmd.Parameters.AddWithValue("id", id);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
         return  resultado;

    }
    public  async Task<int> DeleteFuncionario(int id)
    {
        int resultado;
       
        await using NpgsqlConnection connect=host.Connect();

        await connect.OpenAsync();
        //revisar e colocar pra pegar por id
       await using (var  cmd = new NpgsqlCommand("DELETE FROM funcionario WHERE id = @id ", connect))
        {
            cmd.Parameters.AddWithValue("id",id);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado ;
        
        
    }



}
}