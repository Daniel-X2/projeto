
class Service
{
    public async Task<ListaClient> Get_service()
    {
        Host host =new();
        repository_client n1=new( host);
        ListaClient valores= await n1.Get_client();
        if(valores.lista_client.Count==0)
        {
            throw new ArgumentException("deu zika pq nao tem");
        }
        
        return  valores;
    }
    public async Task<string> add_service(string nome,string cpf,int conta, bool isvip)
    {
        Host host=new();
        repository_client repo=new(host);
        cpf=cpf.Replace(".","");
        if(nome.Length<4)
        {
            throw new ArgumentException("nome pequeno");
        }
        else if(cpf.Length<11 || await repo.VerificarCpf(cpf))
        {
            throw new ArgumentException("cpf incorreto");
        }
        else if(await repo.VerificarConta(conta))
        {
            throw new ArgumentException("conta existente");
        }
       int resultado= await repo.add_client(nome,cpf,conta,isvip);
       switch (resultado){
        case 0:
        {
            throw new ArgumentException("deu merda");    
        }
        case 1:
        {
            return "sucesso";
            
        }
        }
        return "aaaaa";  
    }
    public static async Task Main()
    {
        Host host=new();
        repository_client n1=new(host);
       
       
    }
}
