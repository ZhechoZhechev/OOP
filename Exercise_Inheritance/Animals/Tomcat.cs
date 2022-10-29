namespace Animals
{
    public class Tomcat : Cat
    {
        private const string TOMCAT_GENDER = "male";
        public Tomcat(string name, int age) : base(name, age, TOMCAT_GENDER)
        {
        }
        public override string ProduceSound()
        {
            return "MEOW";
        }
    }
}
