namespace ImmutableStack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack stack = EmptyStack.Instance;
            stack = stack.Push("Hello world!");
            stack = stack.Push("Pineapple");
            stack = stack.Push(null);
            stack = stack.Push(3900);

            while (!stack.IsEmpty())
            {
                Console.WriteLine(stack.Peek());
                stack = stack.Pop();
            }
            Console.ReadLine();
        }
    }
}