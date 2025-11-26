
using Xunit;

namespace CodeQuality.Samples.TestDrivenDevelopment.Submarine;

/// <summary>
/// The submarine has an ExecuteCommand, which can receive a command (string).
/// * "down X" increases your aim by X units.
/// * "up X" decreases your aim by X units.
/// * "forward X" does two things:
///     - It increases your horizontal position by X units
///     - It increases your depth by your aim multiplied by X
/// (Note: that since you're on a submarine, down and up do the opposite of what you might expect: "down" means aiming in the positive direction.)
///
/// Here's an example
/// * forward 5 adds 5 to your horizontal position, a total of 5. Because your aim is 0, your depth does not change.
/// * down 5 adds 5 to your aim, resulting in a value of 5.
/// * forward 8 adds 8 to your horizontal position, a total of 13. Because your aim is 5, your depth increases by 8*5=40.
/// * up 3 decreases your aim by 3, resulting in a value of 2.
/// * down 8 adds 8 to your aim, resulting in a value of 10.
/// * forward 2 adds 2 to your horizontal position, a total of 15. Because your aim is 10, your depth increases by 2*10=20 to a total of 60.
/// After following these new instructions, you would have a horizontal position of 15 and a depth of 60.
///
/// Bonus: Load Input.txt and take your submarine to its course.
/// What result do you get if you multiply your final horizontal position by your final depth?
/// 
/// Source: https://adventofcode.com/2021/day/2
/// </summary>
public class Submarine
{
    public int HorizontalPosition { get; private set; } = 0;
     public int Aim { get; private set; } = 0;
     public int Depth { get; private set; } = 0;

     private record Instruction (string Action ,int Value )
    {
    };
     private Instruction ParseCommand (string command)
    {
        var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string action = parts[0].ToLower();
        return new Instruction(action, int.Parse (parts[1]));
    }
    public void ExecuteCommand(string command)
    {
        var instruction = ParseCommand (command);
            if (instruction.Action == "forward")
        {
            moveForward(instruction);
        }
        else if (instruction.Action == "down")
        {
            moveDown(instruction);
        }
        else if (instruction.Action == "up")
        {
            moveUp(instruction);
        }

        /*
var instruction = ParseCommand (command);
IAction action = instruction.ToAction();
action.MoveSubmarine();

        */
    }

    private void moveUp(Instruction instruction)
    {
        Aim -= instruction.Value;
    }

    private void moveDown(Instruction instruction)
    {
        Aim += instruction.Value;
    }

    private void moveForward(Instruction instruction)
    {
        HorizontalPosition += instruction.Value;
        Depth += Aim * instruction.Value;
    }
}

public class SubmarineTest
{
        [Fact]
        public void Forward5move()
        {
            var submarine = new Submarine();
            submarine.ExecuteCommand("forward 5");
            Assert.Equal(5, submarine.HorizontalPosition);
        }

        [Fact]
        public void Down5mov()
        {
            var submarine = new Submarine();
            submarine.ExecuteCommand("down 5");
            Assert.Equal(5, submarine.Aim);
        }

        [Fact]
        public void Forward8moveafterdown5()
        {
            var submarine = new Submarine();

            submarine.ExecuteCommand("down 5");     
            submarine.ExecuteCommand("forward 8");  

            Assert.Equal(8, submarine.HorizontalPosition); 
            Assert.Equal(40, submarine.Depth);            
        }

        [Fact]
        public void allupanddown()
        {
            var submarine = new Submarine();

            submarine.ExecuteCommand("down 5");
            submarine.ExecuteCommand("up 3"); 
            submarine.ExecuteCommand("down 8");

            Assert.Equal(10, submarine.Aim);
        }

        [Fact]
        public void completemoveexmple()
        {
            var submarine = new Submarine();

            submarine.ExecuteCommand("forward 5");
            submarine.ExecuteCommand("down 5");
            submarine.ExecuteCommand("forward 8");
            submarine.ExecuteCommand("up 3");
            submarine.ExecuteCommand("down 8");
            submarine.ExecuteCommand("forward 2");

            Assert.Equal(15, submarine.HorizontalPosition);
            Assert.Equal(60, submarine.Depth);
            Assert.Equal(10, submarine.Aim);
        }

        [Fact]
        public void input()
        {
             var submarine = new Submarine();
            var commands =
             File.ReadAllLines("TestDrivenDevelopment/Submarine/Input.txt");
            commands.ToList().ForEach(command => submarine.ExecuteCommand(command));
            var result = submarine.HorizontalPosition * submarine.Depth;
            Assert.Equal(2134882034,result);
        }


}