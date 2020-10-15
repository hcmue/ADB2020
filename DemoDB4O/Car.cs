
namespace DemoDB4O
{
    public class Car
    {
        public string Model { get; set; }
        public Pilot Pilot { get; set; }

        public override string ToString()
        {
            return $"{Model} - {Pilot.Name}";
        }
    }


}
