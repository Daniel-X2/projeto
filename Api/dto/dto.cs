

class tipos//esse aqui e o dto do repository client subsituir o task<list<>>
{
    
    public List<client> lista_client {get;set;}= new List<client>();
    public List<funcionario> lista_funcionario {get;set;}= new List<funcionario>();
}
class client
{
    public string Nome{get;set;}
    public string cpf{get;set;}
    public int conta{get;set;}
    public bool isvip{get;set;}
    
}
class funcionario
{
    public string nome{get;set;}
    public string cpf{get;set;}
    public bool isadmin{get;set;}
    public int quantidade_atestato{get;set;}
    public int nascimento{get;set;}
}
