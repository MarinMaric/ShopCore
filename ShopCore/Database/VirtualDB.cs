using ShopCore.WebAPI.Requests;
using ShopCore.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCore.WebAPI.Database
{
    public class VirtualDB
    {
        public static List<Product> Products { get; set; }
        public static List<User> Users { get; set; }
        public static List<Order> Orders { get; set; }
        public static List<OrderItem> OrderItems { get; set; }

        public static int GenerateID(int id)
        {
            return ++id;
        }

        static VirtualDB() {
            Products = new List<Product>();
            Users = new List<User>();
            Orders = new List<Order>();
            OrderItems = new List<OrderItem>();

            PopulateProducts();
            PopulateUsers();
        }

        static void PopulateProducts()
        {
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "FFXIII: LIGHTNING RETURNS",
                Developer = "Square Enix",
                Publisher = "Square Enix",
                Price = 15.99m,
                Description = "Lightning Returns is the concluding chapter of the Final Fantasy XIII saga and series heroine Lightning's final battle. The grand finale of the trilogy brings a world reborn as well as free character customization and stunning action based battles.",
                ReleaseDate = new DateTime(2015, 12, 10),
                Image = "/Images/504353151.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "GRIS",
                Developer = "Nomada Studio",
                Publisher = "Devolver Digital",
                Price = 16.99m,
                Description = "Gris is a hopeful young girl lost in her own world, dealing with a painful experience in her life. Her journey through sorrow is manifested in her dress, which grants new abilities to better navigate her faded reality.",
                ReleaseDate = new DateTime(2018, 12, 13),
                Image = "/Images/grisbanner.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "DEVIL MAY CRY 5",
                Developer = "CAPCOM",
                Publisher = "CAPCOM",
                Price = 59.99m,
                Description = "The ultimate Devil Hunter is back in style, in the game action fans have been waiting for.",
                ReleaseDate = new DateTime(2019, 3, 8),
                Image = "/Images/dmc5cover.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "NI NO KUNI II: REVENANT KINGDOM",
                Developer = "Level-5",
                Publisher = "BANDAI NAMCO Entertainment",
                Price = 59.99m,
                Description = "Join the young king Evan as he sets out on an epic quest to found a new kingdom and, with the help of some new friends, unite his world, saving its people from a terrible evil.",
                ReleaseDate = new DateTime(2018, 3, 23),
                Image = "/Images/ninokuni.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "NIER: AUTOMATA",
                Developer = "Square Enix, PlatinumGames Inc.",
                Publisher = "Square Enix",
                Price = 39.99m,
                Description = "NieR: Automata tells the story of androids 2B, 9S and A2 and their battle to reclaim the machine-driven dystopia overrun by powerful machines.",
                ReleaseDate = new DateTime(2017, 3, 17),
                Image = "/Images/nierautomata.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "THE ELDER SCROLLS V: SKYRIM",
                Developer = "Bethesda Game Studios",
                Publisher = "Bethesda Softworks",
                Price = 14.99m,
                Description = "The next chapter in the highly anticipated Elder Scrolls saga arrives from the makers of the 2006 and 2008 Games of the Year, Bethesda Game Studios. Skyrim reimagines and revolutionizes the open-world fantasy epic, bringing to life a complete virtual world open for you to explore any way you choose.",
                ReleaseDate = new DateTime(2011, 1, 11),
                Image = "/Images/skyrim.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "KINGDOM COME: DELIVERANCE",
                Developer = "Warhorse Studios",
                Publisher = "Warhorse Studios, Deep Silver",
                Price = 29.99m,
                Description = "Kingdom Come: Deliverance is a story-driven open-world RPG that immerses you in an epic adventure in the Holy Roman Empire. Avenge your parents' death as you battle invading forces, go on game-changing quests, and make influential choices.",
                ReleaseDate = new DateTime(2018, 2, 13),
                Image = "/Images/kingdomcome.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "ORI AND THE BLIND FOREST",
                Developer = "Moon Studios GmbH",
                Publisher = "Xbox Game Studios",
                Price = 19.99m,
                Description = "Ori and the Blind Forest tells the tale of a young orphan destined for heroics, through a visually stunning action-platformer crafted by Moon Studios for PC.",
                ReleaseDate = new DateTime(2015, 3, 11),
                Image = "/Images/ori.jpg",
                DateAdded = DateTime.Now
            });
            Products.Add(new Product
            {
                ProductID = VirtualDB.Products.Count(),
                Name = "DEAD CELLS",
                Developer = "Motion Twin",
                Publisher = "Motion Twin",
                Price = 24.99m,
                Description = "Dead Cells is a rogue-lite, metroidvania inspired, action-platformer. You'll explore a sprawling, ever-changing castle... assuming you’re able to fight your way past its keepers in 2D souls-lite combat. No checkpoints. Kill, die, learn, repeat.",
                ReleaseDate = new DateTime(2018, 8, 6),
                Image = "/Images/deadcells.jpg",
                DateAdded = DateTime.Now
            });
        }

        static void PopulateUsers()
        {
            IUserService userService = new UserService();
            UserInsertRequest u = new UserInsertRequest()
            {
                FirstName = "Marin",
                LastName = "Marić",
                Username = "admin",
                Email = "admin@shopcore.com",
                Password = "admin",
                ConfirmPassword = "admin",
            };
            userService.Insert(u);
            Users[0].Admin = true;
        }
    }
}
