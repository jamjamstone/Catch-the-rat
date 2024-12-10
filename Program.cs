using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

namespace minigame1// 구현해볼 기능=> 쥐가 도망을 감<=구현, 쥐가 랜덤하게 도망가는게 아니라 멀어지는 방향으로 도망감, 랭킹 시스템, 본인 닉네임 저장 시스템, 중간에 장애물 생성 해서 미로를 만드는 시스템
/*
 저장과 이어하기 기능? 특수 이벤트=> 랜덤 위치로 커서 날리기, 쥐 2배로 늘리기, => 쥐가 가득차면 게임 오버!,< 일정시간 움직이지 않으면 게임 오버
사냥꾼이니까 덫이나 총쏘기 구현?


 사용한 제작툴과 제작기간 -> 상세하게
무엇을 구현하였는가
그것을 구현하기 위해서 어떤 기법을 사용하였는가


                   이전에 쥐의 움직임을 랜덤으로 구현한 부분 => 코드가 너무 복잡해져서 스위치 케이스로 간략하게 설정
                    x = random.Next(0, 2);
                    y = random.Next(0, 2);
                    if (x == 0 && y == 0)
                    {
                        if (0>i-1&& i+1< 9)
                        {
                            x = 1;
                        }else if (0>j-1&&j + 1 < 9)
                        {
                            y = 1;
                        }
                    } 
                    if (i+x<10&&j+y<10&&i-x>=0&&j-y>=0) {
                        if (arr[i + x, j + y] == 0)
                        {
                            arr[i + x, j + y] = 1;
                            arr[i, j] = 0;
                        }else if (arr[i + x, j - y] == 0)
                        {
                            arr[i + x, j - y] = 1;
                            arr[i, j] = 0;
                        } else if (arr[i - x, j + y] == 0)
                        {
                            arr[i - x, j + y] = 1;
                            arr[i, j] = 0;
                        }else if (arr[i - x, j - y] == 0)
                        {
                            arr[i - x, j - y] = 1;
                            arr[i, j] = 0;
                        }
                    }



 */
{
    internal class Program
    {
        struct Scores
        {
            public int score;
            public int leftChance;
        }
        static void mouseRun(int[,] arr)
        {
            Random random = new Random();
            int whereMousemove;
            bool sw = true;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 1)
                    {
                        sw = true;
                        while (sw)
                        {
                        //randomRestart:
                            whereMousemove = random.Next(1, 5);
                            switch (whereMousemove)
                            {
                                case 1:
                                    if (j + 1 < 10 && arr[i, j + 1] == 0)// 배열논리를 앞으로 설정했더니 배열 범위가 넘어갈 경우 바로 오류가 발생 따라서 배열 범위가 넘어가는지 먼저 확인하게 설정하였다.
                                    {
                                        arr[i, j + 1] = 1;
                                        arr[i, j] = 0;
                                        sw=false;

                                    }
                                    else
                                    {
                                        //goto randomRestart;
                                        continue;
                                    }
                                    break;
                                case 2:
                                    if (j - 1 >= 0 && arr[i, j - 1] == 0)
                                    {
                                        arr[i, j - 1] = 1;
                                        arr[i, j] = 0;
                                        sw = false;
                                    }
                                    else
                                    {
                                        //goto randomRestart;
                                        continue;
                                    }
                                    break;
                                case 3:
                                    if (i - 1 >= 0 && arr[i - 1, j] == 0)
                                    {
                                        arr[i - 1, j] = 1;
                                        arr[i, j] = 0;
                                        sw = false;
                                    }
                                    else
                                    {
                                        //goto randomRestart;
                                        continue;
                                    }
                                    break;
                                case 4:
                                    if (i + 1 < 10 && arr[i + 1, j] == 0)
                                    {
                                        arr[i + 1, j] = 1;
                                        arr[i, j] = 0;
                                        sw = false;
                                    }
                                    else
                                    {
                                        //goto randomRestart;
                                        continue;
                                    }
                                    break;
                                default:
                                    //goto randomRestart;
                                    continue;


                            }
                        }
                    }

                }

            }
        }
        static void resetMouse(int[,] arr, ref bool isGameRunning)
        {
            int mouseRefillCount = 0;
            //int mouseNum = 0;
            Random randMouse = new Random();
            int x, y;
            /*for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 1)
                    {
                        mouseNum += 1;
                    }

                }

            }*/
            /*
            if (mouseNum == 100)
            {
                isGameRunning = false;
            }*///특수 이벤트인 쥐 2배 생성시 게임오버를 위해서 작성한 코드 현재는 특수 이벤트가 없어서 필요 없다. 쥐 2배를 밟았을 때 쥐가 가득차면 게임 오버되게 만드는 코드
            {

            }
            while (mouseRefillCount < 5)// 5마리의 쥐를 랜덤하게 추가해주는 기능
            {
                x = randMouse.Next(0, 10);
                y = randMouse.Next(0, 10);
                if (arr[x, y] == 0)
                {
                    arr[x, y] = 1;
                    mouseRefillCount++;
                }
            }
        }
        static void drawGame(int[,] arr,ref Scores score) //게임판 만들어주는 함수 -> 여기서 특수칸의 숫자를 3부터 할당해서 특수칸도 구현 가능
        {
            Console.Clear();   
            for(int i=0; i<arr.GetLength(0)*2; i++)
            {
                for(int j=0; j<arr.GetLength(1)*2; j++)  ///아스키 아트 같은 걸로도 구현 가능, 지금과 같이 도형 구성을 하나씩 하지 않고 정해진 이미지를 몇개 설정해서 구현하는 방법도 있음
                    // => 미로 구현도 비슷하게 가능 여러 종류의 미로 칸 이미지를 저장한 뒤에 이를 이용해서 구현 가능! => 다만 안에 있는 정보 자체는 배열이나 큐를 사용해서 구현해야 할 것
                {
                    if (arr[i/2, j/2] == 0)
                    {
                        //빈칸 정사각형이나 직사각형으로 표시  
                        if (i % 2 == 0)
                        {
                            Console.Write("-");
                        }
                        else if (i % 2 == 1 && j % 2 == 0)
                        {
                            Console.Write("|");
                        }
                        else if(j%2==1){
                            Console.Write(" ");
                        }
                        
                        
                        

                     
                    }else if(arr[i/2, j/2] == 1)
                    {
                        // 두더지만 존재 원으로 표시
                        if (i % 2 == 0)
                        {
                            Console.Write("-");
                        }
                        else if (i % 2 == 1 && j % 2 == 0)
                        {
                            Console.Write("|");
                        }
                        else if (j % 2 == 1)
                        {
                            Console.Write("O");
                        }
                        


                    }
                    else if( arr[i/2, j/2] == 2)
                    {
                        //커서 만 존재 #로 표시
                        if (i % 2 == 0)
                        {
                            Console.Write("-");
                        }
                        else if (i % 2 == 1 && j % 2 == 0)
                        {
                            Console.Write("|");
                        }
                        else if (j % 2 == 1)
                        {
                            Console.Write("#");
                        }
                        
                    }
                    if (j == arr.GetLength(1)*2-1)
                    {
                        if (i % 2 == 0)
                        {
                            Console.Write("-");
                        }
                        else
                        {
                            Console.Write("|");
                        }
                    }
                    
                }
                Console.WriteLine();
            }
            for(int i = 0; i < arr.GetLength(1) * 2; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine($"현재 점수는 {score.score}입니다.");
            Console.WriteLine($"남은 이동횟수는 {score.leftChance}입니다.");

            
            


        }
        
        
        static int MoveCursor(int[,] arr,ref Scores nowscore/*키보드 입력값, 전체 게임판 배열*/)//마우스 커서 움직이는 함수 + 점수 계산도 같이 해준다
        {
            
            var pressedKey= Console.ReadKey();    
            if(pressedKey.Key == ConsoleKey.Q)
            {
                return 0;
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for(int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j]==2)
                    {
                        //키보드 입력값에 따라서 변경
                        switch (pressedKey.Key)
                        {
                            case ConsoleKey.UpArrow: //키보드 위
                                if (i>0)
                                {
                                    if (arr[i - 1, j] == 1)
                                    {
                                        arr[i - 1, j] = 2;
                                        //점수 증가
                                        nowscore.score += 10;
                                        nowscore.leftChance += 5;
                                    }
                                    else
                                    {
                                        arr[i - 1, j] = 2;
                                    }
                                    arr[i, j] = 0;
                                }
                                nowscore.leftChance -= 1;
                                mouseRun(arr);
                                drawGame(arr, ref nowscore);
                                return 1;
                                //break;
                            case ConsoleKey.DownArrow: //키보드 아래 오류!
                                if (i < 9)
                                {
                                    if (arr[i + 1, j] == 1)
                                    {
                                        arr[i + 1, j] = 2;
                                        //점수 증가
                                        nowscore.score += 10;
                                        nowscore.leftChance += 5;
                                    }
                                    else
                                    {
                                        arr[i + 1, j] = 2;
                                    }
                                    arr[i, j] = 0;
                                }
                                
                                nowscore.leftChance -= 1;
                                mouseRun(arr);
                                drawGame(arr, ref nowscore);
                                return 1;
                                //case 0 와 유사하게 작성 이하 동일
                                //break;
                            case ConsoleKey.LeftArrow:  //키보드 왼쪽
                                if (j > 0)
                                {
                                    if (arr[i, j-1] == 1)
                                    {
                                        arr[i, j-1] = 2;
                                        //점수 증가
                                        nowscore.score += 10;
                                        nowscore.leftChance += 5;
                                    }
                                    else
                                    {
                                        arr[i, j-1] = 2;
                                    }
                                    arr[i, j] = 0;
                                }
                                nowscore.leftChance -= 1;
                                mouseRun(arr);
                                drawGame(arr, ref nowscore);
                                return 1;
                               // break;
                            case ConsoleKey.RightArrow:  //키보드 오른쪽 오류! 이중 for문을 사용했기 때문에 for문이 진행하는 방향으로 이동하면 계속 작동하여 커서를 항상 끝으로 밀어넣었음
                                if (j < 9)
                                {
                                    if (arr[i, j+1] == 1)
                                    {
                                        arr[i, j+1] = 2;
                                        //점수 증가
                                        nowscore.score += 10;
                                        nowscore.leftChance += 5;

                                    }
                                    else
                                    {
                                        arr[i, j+1] = 2;
                                    }
                                    arr[i, j] = 0;
                                }
                                nowscore.leftChance -= 1;
                                mouseRun(arr);
                                drawGame(arr, ref nowscore);
                                return 1;
                                //break;
                            /*case ConsoleKey.Q ://q 키 입력시 0반환 원래 esc 키였으나 esc키 사용시 마지막 게임종료 문구 출력시 한글자가 없이 출력되어 q로 변경하였음
                                Console.WriteLine("종료");
                                return 0;*/
                                
                            default: //이외의 입력

                               break;

        
        
                        }
                       
                    }
                }
            }
        
            mouseRun(arr);
            drawGame(arr,ref nowscore);
            return 1;
            
        }

        static void Main(string[] args)//랜덤 함수와 시간함수를 사용해야 함 그외 전역 변수 사용필요
        {

            Scores nowscore;
            nowscore.score = 0;
            bool isGameRunning = true; // 게임 실행 상태 확인중 
            int[,] mousePlace= new int[10,10];// 전체 게임판
            //두더쥐가 없으면 0 있으면 1 커서는 2 커서와 두더쥐가 함께 있으면 3으로 설정
            Random randMouse = new Random();//랜덤함수를 통해서 두더지 위치 설정 
            //mousePlace[0, 1] = 1;
            //mousePlace[5, 2] = 2;
            //mousePlace[6, 3] = 1;
            Stopwatch timer = new Stopwatch();
            resetMouse(mousePlace,ref isGameRunning);
            mousePlace[randMouse.Next(0, 10), randMouse.Next(0, 10)] = 2;
            //게임판 for문과 draw문을 사용해서 최초 제작 => 게임판 제작을 함수로 설정하자
            Console.WriteLine("최초 이동횟수를 설정하세요!(20이하!)");
            while (true)
            {
                int.TryParse(Console.ReadLine(), out nowscore.leftChance);
                if (nowscore.leftChance <= 0 || nowscore.leftChance > 20)
                {
                    Console.WriteLine("1부터 20까지의 정수를 다시 입력해 주세요!");
                }
                else
                {
                    Console.WriteLine($"초기 이동횟수는 {nowscore.leftChance}입니다.");
                    nowscore.score=400-(nowscore.leftChance*10);
                    while (true)
                    {
                        Console.WriteLine($"시작 점수는 {nowscore.score}입니다!. \n게임을 시작하려면 [start]를 입력하세요");
                        Console.WriteLine("게임을 종료하려면 게임 화면에서 q키를 눌러주세요");
                        if (Console.ReadLine() =="start")
                        {
                            break;
                        }
                    }
                    break;
                }
            }
            
            drawGame(mousePlace,ref nowscore);
            Console.WriteLine();
            Console.WriteLine();
            Stopwatch limit = new Stopwatch();
            timer.Start();
            limit.Start();
            while (isGameRunning)//실제 게임동안 실행될 코드들 
            {
                if(timer.ElapsedMilliseconds > 5000)// 쥐를 전부 잡았을 경우 추가적인 게임진행이 안되기 때문에 이를 막기 위해서 일정 시간마다 쥐를 추가함
                {
                    resetMouse(mousePlace,ref isGameRunning);
                    drawGame(mousePlace,ref nowscore);
                    timer.Restart();
                }

                if (Console.KeyAvailable)
                {
                    if (MoveCursor(mousePlace, ref nowscore) == 0)// 방향키를 입력받아 커서를 이동
                    {
                        isGameRunning = false;
                    }
                    limit.Restart();

                    //Console.WriteLine(timer.ElapsedMilliseconds);





                }
                if (nowscore.leftChance == 0)
                {
                    isGameRunning = false;
                }
                if (limit.ElapsedMilliseconds > 50000)// 커서를 움직이지 않고 가만히 있으면 언젠가는 쥐가 추가될 것이므로 무한히 기다리는것을 막기 위해서 일정 시간이 지나면 게임오버되게 설정
                {
                    isGameRunning = false;
                }
            }
            Console.Clear();
            Console.WriteLine("게임 종료!"); //esc키를 눌러 종료하면 게임종료 문구의 앞부분이 출력되지 않기 때문에 q로변경
            Console.WriteLine($"최종점수는 {nowscore.score}입니다.");
            timer.Stop();
            limit.Stop();


        }
    }
}
