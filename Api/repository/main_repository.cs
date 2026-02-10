


using Npgsql;
using static System.Console;

 class  Init_repository
{
    private static string n2=File.ReadAllText("/home/daniel/Pasta_boa_demais_pra_ficar_em_um_lugar/Nova pasta/projeto/Api/host.txt");
   
    private protected static NpgsqlConnection Connect()
    {
        NpgsqlConnection n1=new (n2);
        
        return n1;
    }
   
    
    
}







