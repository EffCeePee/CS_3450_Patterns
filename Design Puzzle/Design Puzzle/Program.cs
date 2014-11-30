using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Puzzle
{
    class Program
    {


        static void Main(string[] args)
        {
            const int ZERO = 0;
            const int ONE = 1;
            const int TWO = 2;
            const int THREE = 3;
            const int FOUR = 4;
                         
            string king = "push 1 to see what the King does.";
            string queen = "push 2 to see what the Queen does.";
            string knight = "push 3 to see what the Knight does.";
            string troll = "push 4 to see what the Troll does.";
            string exit = "press 0 to end the program";
            string another = "please make another selection or ";
            int selection = 0;
            int selection2 = 0;

            string sword = "push 1 to choose a sword.";
            string axe = "push 2 to choose an axe.";
            string knife = "push 3 to choose a knife.";
            string bowAndArrow = "push 4 to choose a bow and arrow.";

            Console.WriteLine("{0}", king);
            Console.WriteLine("{0}", queen);
            Console.WriteLine("{0}", knight);
            Console.WriteLine("{0}", troll);

            string temp = Console.ReadLine();
            selection = Convert.ToInt32(temp);

            Console.WriteLine("{0}", sword);
            Console.WriteLine("{0}", axe);
            Console.WriteLine("{0}", knife);
            Console.WriteLine("{0}", bowAndArrow);

            string temp2 = Console.ReadLine();
            selection2 = Convert.ToInt32(temp2);

            
            
            
            do
            {

                switch (selection)
                {
                    case ONE:
                        King K = new King();                      
                        K.Fight();
                        if(selection2 == ONE)
                        {
                            WeaponBehavior S = new SwordBehavior(); 
                            K.setWeapon(S);
                            S.useWeapon();
                        }

                        if(selection2 == TWO)
                        {
                            WeaponBehavior A = new AxeBehavior();
                            K.setWeapon(A);
                            A.useWeapon();
                        }


                        if(selection2 == THREE)
                        {
                            WeaponBehavior N = new KnifeBehavior();
                            K.setWeapon(N);
                            N.useWeapon();
                        }

                        if(selection2 == FOUR)
                        {
                            WeaponBehavior B = new BowAndArrowBehavior();
                            K.setWeapon(B);
                            B.useWeapon();
                        }

                        Console.WriteLine("{0} {1}", another, exit);
                        temp = Console.ReadLine();
                        selection = Convert.ToInt32(temp);
                        break;
                      

                    case TWO:
                        Queen Q = new Queen();
                        Q.Fight();
                        
                        Console.WriteLine("{0} {1}", another, exit);
                        temp = Console.ReadLine();
                        selection = Convert.ToInt32(temp);
                        break;

                    case THREE:
                        Knight I = new Knight();
                        I.Fight();
                        if(selection2 == ONE)
                        {
                            WeaponBehavior S = new SwordBehavior(); 
                            I.setWeapon(S);
                            S.useWeapon();
                        }

                        if(selection2 == TWO)
                        {
                            WeaponBehavior A = new AxeBehavior();
                            
                            I.setWeapon(A);
                            A.useWeapon();
                        }


                        if(selection2 == THREE)
                        {
                            WeaponBehavior N = new KnifeBehavior();
                            I.setWeapon(N);
                            N.useWeapon();
                        }

                        if(selection2 == FOUR)
                        {
                            WeaponBehavior B = new BowAndArrowBehavior();
                            I.setWeapon(B);
                            B.useWeapon();
                        }

                        Console.WriteLine("{0} {1}", another, exit);
                        temp = Console.ReadLine();
                        selection = Convert.ToInt32(temp);
                        break;

                    case FOUR:
                        Troll T = new Troll();
                        T.Fight();

                        if(selection2 == ONE)
                        {
                            WeaponBehavior S = new SwordBehavior(); 
                            T.setWeapon(S);
                            S.useWeapon();
                        }

                        if(selection2 == TWO)
                        {
                            WeaponBehavior A = new AxeBehavior();
                            T.setWeapon(A);
                            A.useWeapon();
                        }


                        if(selection2 == THREE)
                        {
                            WeaponBehavior N = new KnifeBehavior();
                            T.setWeapon(N);
                            N.useWeapon();
                        }

                        if(selection2 == FOUR)
                        {
                            WeaponBehavior B = new BowAndArrowBehavior();
                            T.setWeapon(B);
                            B.useWeapon();
                        }
                        Console.WriteLine("{0} {1}", another, exit);
                        temp = Console.ReadLine();
                        selection = Convert.ToInt32(temp);
                        break;

                    default:
                        Console.WriteLine("{0}", ZERO);
                        break;
                }


                
            } while (selection != 0);


        }// end main
    }// end class



public abstract class Character
{
    WeaponBehavior weapon;
    public abstract void Fight();
    public void setWeapon(WeaponBehavior w)
    {
        this.weapon = w;
    }
      
}


class King: Character
{
    public override void Fight()
    {
        string fighting = "I'm a king.";
        Console.WriteLine("{0}", fighting);
    }

}

class Queen : Character
{
    public override void Fight()
    {
        string fighting = "Im a queen.";
        Console.WriteLine("{0}", fighting);
        
    }
}

class Knight : Character
{
    public override void Fight()
    {
        string fighting = "I'm a Knight.";
        Console.WriteLine("{0}", fighting);
        
    }

}

class Troll : Character
{
    public override void Fight()
    {
        string fighting = "I'm a Troll.";
        Console.WriteLine("{0}", fighting);
        
    }
}

public interface WeaponBehavior
{
    void useWeapon();
}

class SwordBehavior : WeaponBehavior
{
    public void useWeapon()
    {
        string sword = "I swing my sword through the air and cut you!";
        Console.WriteLine("{0}", sword);
    }
}


class KnifeBehavior : WeaponBehavior
{
    public void useWeapon()
    {
        string knife = "I thrust forward and cut you!";
        Console.WriteLine("{0}", knife);
    }
}

class BowAndArrowBehavior : WeaponBehavior
{
    public void useWeapon()
    {
        string bow = "I shoot my bow and arrow at you!";
        Console.WriteLine("{0}", bow);
    }
}

class AxeBehavior : WeaponBehavior
{
    public void useWeapon()
    {
        string axe = "I bring my axe down through he air and behead you!";
        Console.WriteLine("{0}", axe);
    }
}
    
}// end namespace
