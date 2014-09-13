
namespace NaPegada.Util
{
    public interface IInjecao<T>
        where T : class
    {
        void Injetar(T tipo);
    }

    public interface IInjecao<T1, T2>
        where T1 : class
        where T2 : class
    {
        void Injetar(T1 tipo_1, T2 tipo_2);
    }
    public interface IInjecao<T1, T2, T3>
        where T1 : class
        where T2 : class
        where T3 : class
    {
        void Injetar(T1 tipo_1, T2 tipo_2, T3 tipo_3);
    }

    public interface IUniao<T1, T2>
        where T1 : class
        where T2 : class
    {
        void Unir(T1 tipo_1, T2 tipo_2);
    }
}
