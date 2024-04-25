using System.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static ConsoleApp3.Program;

namespace ConsoleApp3
{
    internal class Program
    {
        private static Player player;
        private static ItemData itemData;
        static List<ItemData> itemDatas = new List<ItemData>(); //아이템 리스트 작성
        static List<ItemData> playerEquipped = new List<ItemData>(); //플레이어 장착 인벤토리 작성
        static void Items()
        {
            itemDatas.Add(new ItemData(1, "수련자 갑옷",0, 0, 5, "수련에 도움을 주는 갑옷입니다."));
            itemDatas.Add(new ItemData(2, "무쇠갑옷", 0, 0, 9, "질긴 천을 덧대어 제작한 낡은 갑옷입니다."));
            itemDatas.Add(new ItemData(3, "스파르타의 갑옷", 0, 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."));
            itemDatas.Add(new ItemData(4, "낡은 검",1, 2, 0, "쉽게 볼 수 있는 낡은 검 입니다."));
            itemDatas.Add(new ItemData(5, "청동 도끼",1, 5, 0, "어디선가 사용됐던거 같은 도끼입니다."));
            itemDatas.Add(new ItemData(6, "스파르타의 창",1, 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다."));
        }
        public class ItemData //아이템 데이터 작성
        {
            public int ItemId;
            public string ItemName;
            public int ItemPower;
            public int ItemDef;
            public string ItemComment;
            public int Itemtype; // 0 = 방어구 1 =  무기
            public bool PlayerEquipped; //장착유무 체크

            public ItemData(int itemId, string itemName, int itemtype, int itempower, int itemDefense, string itemComment)
            {
                ItemId = itemId;
                ItemName = itemName;
                ItemPower = itempower;
                ItemDef = itemDefense;
                Itemtype = itemtype;
                ItemComment = itemComment;
                PlayerEquipped = false;
            }
        }
        static void Inventory()
        {
            Console.Clear();
            Console.WriteLine("[인벤토리]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");

            string playerEquipped;
            for (int i = 1; i < itemDatas.Count + 1; i++)
            {
                ItemData item = itemDatas[i - 1];
                Console.Write($"{item.ItemId}.");
                playerEquipped = item.PlayerEquipped ? "[E]" : ""; //플레이어가 입고 있다면 [E] 입력
                Console.Write($"{playerEquipped}");
                Console.Write($"{item.ItemName}");
                PlusItemStat(item); // 스테이터스에 스탯 입력하기
                Console.WriteLine(item.ItemComment);
            }
            Console.WriteLine("\n1.장착관리");
            Console.WriteLine("2.나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidAction(1, 2);

            switch(input)
            {
                case 1:
                    ManagementPlayerEquip();
                    break;
                case 2:
                    TxtGame();
                    break;
            }
        }
        static void PlusItemStat(ItemData item)
        {
            if (item.Itemtype == 1)
            {
                Console.Write($"| 공격력 + {item.ItemPower} |");
            }
            else if (item.Itemtype == 0)
            {
                Console.Write($"| 방어력 + {item.ItemDef} |");
            }
        }

        static void ManagementPlayerEquip()
        {
            Console.Clear();
            Console.WriteLine("[인벤토리 - 장착관리]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");

            string playerEquipped;
            for (int i = 1; i < itemDatas.Count+1; i++)
            {
                ItemData item = itemDatas[i-1];
                Console.Write($"{item.ItemId}.");
                playerEquipped = item.PlayerEquipped ? "[E]" : ""; //플레이어가 입고 있다면 [E] 입력
                Console.Write($"{playerEquipped}");
                Console.Write($"{item.ItemName}");
                PlusItemStat(item); // 장비 스탯 입력
                Console.WriteLine(item.ItemComment);
            }

            Console.WriteLine("\n0.나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidAction(0, itemDatas.Count);

            if (input == 0)
            {
                Inventory();
            }
            if (input > 0 && input <= itemDatas.Count)
            {
                ItemData SeletItem = itemDatas[input - 1];
                ChangeEquip(SeletItem);
                ManagementPlayerEquip();
            }

        }

        static void ChangeEquip(ItemData item)
        {
            if(item.PlayerEquipped == false)
            {
                if (item.Itemtype == 1)
                {
                    for (int i = 1; i < itemDatas.Count + 1; i++)
                    {
                        ItemData checkitem = itemDatas[i - 1];
                        if(checkitem.PlayerEquipped == true && checkitem.Itemtype == 1)
                        {
                            checkitem.PlayerEquipped = false;
                            player.Power = player.BasePower.ToString();
                        }
                    }
                    item.PlayerEquipped = true;
                    player.Power = player.Power + $"[+{item.ItemPower}]";
                }
                else if (item.Itemtype == 0)
                {
                    for (int i = 1; i < itemDatas.Count+1; i++)
                    {
                        ItemData checkitem = itemDatas[i - 1];
                        if (checkitem.PlayerEquipped == true && checkitem.Itemtype == 0)
                        {
                            checkitem.PlayerEquipped = false;
                            player.Defence = player.BaseDefence.ToString();
                        }
                    }
                    item.PlayerEquipped = true;
                    player.Defence = player.Defence + $"[+{item.ItemDef}]";
                }
            }
            else
            {
                item.PlayerEquipped = false;

                if (item.Itemtype == 1)
                {
                    player.Power = player.BasePower.ToString();
                }
                else if (item.Itemtype == 0)
                {
                    player.Defence = player.BaseDefence.ToString();
                }
            }
            
        }

        static void PlayerData()
        {
            Console.Clear();
            player = new Player("전사", 1, 10, "10", 5, "5", 100, 1500);
        }
        public class Player
        {
            public string PlayerClass { get; set; }
            public int Level { get; set; }
            public string Power { get; set; }
            public string Defence { get; set; }
            public int Helth { get; set; }
            public int Gold { get; set; }
            public int BasePower { get; set; }
            public int BaseDefence {  get; set; }

            public Player(string playerClass, int level,int basePower, string power, int baseDefence, string defence, int helth, int gold)
            {
                PlayerClass = playerClass;
                Level = level;
                Power = power;    
                Defence = defence;
                Helth = helth;
                Gold = gold;
                BasePower = basePower;
                BaseDefence = baseDefence;
            }

        }
        static void Status()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine("Lv. "+ player.Level);
            Console.WriteLine(player.PlayerClass);
            Console.WriteLine("공격력 : "+player.Power);
            Console.WriteLine("방어력 : "+player.Defence);
            Console.WriteLine("체력 : "+player.Helth);
            Console.WriteLine("Gold : "+player.Gold+"G\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidAction(0, 0);

            switch(input)
            {
                case 0:
                    TxtGame();
                    break;
            }
        }

        static void TxtGame()
        {
            Console.Clear();
            WriteOpening();
            int input = CheckValidAction(0, 3);

            switch (input)
            {
                case 0:
                    Environment.Exit(0); 
                    break;
                case 1:
                    Status();
                    break;
                case 2:
                    Inventory();
                    break;
                case 3:

                    break;
            }
        }

        static void WriteOpening()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("0. 게임 종료");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }

        static int CheckValidAction(int min, int max) //값 입력에 대한 분석 매서드
        {
            while (true)
            {
                string action = Console.ReadLine();

                bool parseSuccess = int.TryParse(action, out var ret); //받은 문자열을 정수로 변환하고 성공했다면 Bool값을 True로 변경
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max) //min~max 사이에 존재하는 값이라면 값 반환
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다."); // 아니라면 오류 실행
            }
        }
        // 게임 실행
        static void Main()
        {
            Items();
            PlayerData();
            TxtGame();

            //게임 시작화면, 원하는 행동의 숫자를 타이핑하면 실행됨
            //1상태보기 - 캐릭터의 정보를 표시, 레벨/이름/직업/공격력/방어력/체력/Gold
            //2인벤토리 - 보유중인 아이템을 전부 보여줌 장착중인 아이템에는 [E] 사용
            //2-1 장착관리 - 아이템 목록 앞에 숫자 표시, 일치하는 아이템을 선택했다면
            // 장착중이지 않았다면 - [E] 추가 장착중이라면 - 장착해제 
            // 번호선택이 틀렸다면 - 잘못된 입력입니다
            // 아이템의 중복장착 허용 (무기 방어구 여러 개) 아이템이 적용 되었다면 상태보기에 정보 반영
            //3 보유중인 골드와 아이템의 정보 가격 표시
            // 아이템 정보 오른쪽에는 가격, 이미 구매한 아이템은 구매 완료
            // 아이템 구매를 선택했다면 목록 앞에 숫자 넣음, 이미 구매한 아이템이라면 메세지 출력
            // 금액이 충분하면 구매 완료/ 모자르면 골드 부족/ 잘못 입력하면 잘못 입력
        }
    }
}
