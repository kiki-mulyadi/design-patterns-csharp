// See https://aka.ms/new-console-template for more information

internal class StateContainer
{
    private string _state;

    public StateContainer(string state)
    {
        this._state = state;
    }

    public void ChangeState(string state)
    {
        this._state = state;
    }

    public string GetState()
    {
        return this._state;
    }
}

internal class ClientClass
{
    private readonly StateContainer _data;

    public ClientClass(StateContainer m)
    {
        this._data = m;
    }

    public void ChangeMyClassState(string state)
    {
        _data.ChangeState(state);
    }

    public void ShowState()
    {
        string msg = _data.GetState();
        Console.WriteLine(msg);
    }
}

internal class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello, World!\n");

        string str = "Initial state";
        StateContainer stateContainer = new StateContainer(str);
        ClientClass clientClass = new ClientClass(stateContainer);

        clientClass.ShowState();

        Console.WriteLine(stateContainer.GetState());

        string str2 = "Second state";
        clientClass.ChangeMyClassState(str2);
        clientClass.ShowState();

        Console.WriteLine(stateContainer.GetState());
    }
}