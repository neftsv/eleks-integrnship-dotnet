using InternetShop.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Net;

namespace InternetShop.Data
{
    public class Seed
    {
        public static async void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Moderator))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator));
     
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Users>>();

                    var electronics = new Categories { Name = "Електроніка" };
                    var householdGoods = new Categories { Name = "Побутові товари" };
                    var householdAppliances = new Categories { Name = "Побутова техніка" };
                    var personalCare = new Categories { Name = "Особиста гігієна" };
                    var officeFurniture = new Categories { Name = "Офісні меблі" };
                    var photoAndVideoTechnology = new Categories { Name = "Фото та відео техніка" };

                    await context.AddRangeAsync(electronics,
                        householdGoods,
                        householdAppliances,
                        personalCare,
                        officeFurniture,
                        photoAndVideoTechnology);

                    var user1 = new Users
                    {
                        UserName = "fakeMail@mail.com",
                        Email = "fakeMail@mail.com",
                        PhoneNumber = "1234567890",
                        Surname = "UserSurname",
                        Name = "UserName",
                        Patronimic = "UserPatronimic",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Smartphone X12",
                                    Description = "Остання модель із високоякісною камерою та довготривалою батареєю.",
                                    Price = 999.99m,
                                    Categories = electronics,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://m.media-amazon.com/images/I/71rYtW-VGLL._AC_SX679_.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://m.media-amazon.com/images/I/81zZ+MmEjLL._AC_SX679_.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://m.media-amazon.com/images/I/81WsitJHbZL._AC_SX679_.jpg"
                                        }
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products
                                {
                                    Name = "Екологічна водна пляшка",
                                    Description = "Багаторазова пляшка без BPA, об'ємом 1 літр.",
                                    Price = 14.99m,
                                    Categories = householdGoods,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/7854824.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/7854826.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products =  new Products
                                {
                                    Name = "Бездротові навушники",
                                    Description = "Навушники з шумозаглушенням, накладні, з Bluetooth з'єднанням.",
                                    Price = 199.99m,
                                    Categories = electronics,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/280047779.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/280047777.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/280047771.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/280047780.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/280047773.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products {
                                    Name = "Електрична зубна щітка",
                                    Description = "Акумуляторна зубна щітка з інтелектуальним таймером і сенсором тиску.",
                                    Price = 129.99m,
                                    Categories = personalCare,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/233778172.jpg"
                                        }
                                    }
                                },
                            }
                        }
                    };

                    await userManager.CreateAsync(user1, "1fakeMail@mail.com");
                    await userManager.AddToRoleAsync(user1, UserRoles.User);

                    var product1 = new Products
                    {
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                UserId = user1.Id,
                                Users = user1,
                            }
                        },
                        Name = "Портативна кавоварка для еспресо",
                        Description = "Компактна та легка кавоварка для кави на винос.",
                        Price = 64.99m,
                        Categories = householdAppliances,
                        Images = new List<Images>
                        {
                            new Images
                            {
                                Url = "https://remontservis.kiev.ua/image/cache/webp/catalog/prodazha-kofevarok/05904/1-1000x1000.webp"
                            },
                            new Images
                            {
                                Url = "https://remontservis.kiev.ua/image/cache/webp/catalog/prodazha-kofevarok/05904/6-1000x1000.webp"
                            },
                            new Images
                            {
                                Url = "https://remontservis.kiev.ua/image/cache/webp/catalog/prodazha-kofevarok/05904/5-1000x1000.webp"
                            },
                         }
                    };
                    await context.AddAsync(product1);

                    var user2 = new Users
                    {
                        UserName = "fakeMail2@mail.com",
                        Email = "fakeMail2@mail.com",
                        PhoneNumber = "9638520741",
                        Surname = "User2Surname",
                        Name = "User2Name",
                        Patronimic = "User2Patronimic",
                        UsersProducts = new List<UsersProducts>
                        {
                            new UsersProducts
                            {
                                Products = new Products 
                                { 
                                    Name = "Ігровий ноутбук", 
                                    Description = "Високопродуктивний ноутбук, призначений для ігор, з топовою графікою та процесором.", 
                                    Price = 2499.99m,
                                    Categories = electronics,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://itmag.ua/upload/resize_cache/iblock/c3a/c4w7ryfyvh5sk8v6hfzofdvnuc9lz7j5/350_400_1/26sm.webp"
                                        },
                                        new Images
                                        {
                                            Url = "https://itmag.ua/upload/resize_cache/iblock/8a9/ilf2fu7fb7u5xb59je7pm1ki59apu5oy/350_400_1/27sm.webp"
                                        },
                                        new Images
                                        {
                                            Url = "https://itmag.ua/upload/resize_cache/iblock/d51/wmur29xl67c756x1oerocscf91b16d07/350_400_1/28sm.webp"
                                        },
                                        new Images
                                        {
                                            Url = "https://itmag.ua/upload/resize_cache/iblock/5ae/wxk9inf22ytvk4pfkl2xkouzpwb0h8zs/350_400_1/29sm.webp"
                                        }
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products { 
                                    Name = "Ергономічне офісне крісло", 
                                    Description = "Регульоване та комфортабельне офісне крісло, розроблене для підтримки правильної постави.", 
                                    Price = 399.99m,
                                    Categories = officeFurniture,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://kulik-system.com/upload/webp/iblock/a94/g8j99aljimbc0ykfygzdyjgrlr1acczz/SPACE_1925_0511_veshalka.webp"
                                        }
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products 
                                {
                                    Name = "Bluetooth колонка", 
                                    Description = "Портативна колонка з високоякісним звуком і водонепроникним дизайном.", 
                                    Price = 119.99m,
                                    Categories = electronics,
                                    Images = new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content.rozetka.com.ua/goods/images/big/238375515.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/238375514.jpg"
                                        },
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/238375519.jpg"
                                        },
                                    }
                                },
                            },
                            new UsersProducts
                            {
                                Products = new Products 
                                { 
                                    Name = "4K Дрон",
                                    Description = "Дрон з 4K камерою для аерофотозйомки та відеозапису.", 
                                    Price = 749.99m,
                                    Categories = photoAndVideoTechnology,
                                    Images= new List<Images>
                                    {
                                        new Images
                                        {
                                            Url = "https://content2.rozetka.com.ua/goods/images/big/300704779.jpg",
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/300704780.jpg",
                                        },
                                        new Images
                                        {
                                            Url = "https://content1.rozetka.com.ua/goods/images/big/300704781.jpg",
                                        }
                                    }
                                }
                            }
                        },
                    };

                    await userManager.CreateAsync(user2, "fakeMail2@mail.com");
                    await userManager.AddToRoleAsync(user2, UserRoles.User);

                    var carProduct = new CartsProducts
                    {
                        ProductId = product1.Id,
                        Products = product1,
                        Carts = new Carts
                        {
                            Users = user2,
                        },
                        Quantity = 1,
                    };
                    
                    context.Add(carProduct);

                    var order = new OredersProducts
                    {
                        ProductId = product1.Id,
                        Products = product1,
                        Orders = new Orders
                        {
                            Date = DateTime.Now,
                            TotalPrice = product1.Price,
                            UserId = user1.Id,
                            Users = user1,
                        },
                        Quantity = 1,
                    };

                    context.Add(order);

                    var admin = new Users
                    {
                        UserName = "fakeAdminMail@mail.com",
                        Email = "fakeAdminMail@mail.com",
                        PhoneNumber = "0987654321",
                        Surname = "AdminSurname",
                        Name = "AdminName",
                        Patronimic = "AdminPatronimic",
                    };
                    await userManager.CreateAsync(admin, "1fakeAdminMail@mail.com");
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);


                    var moderator = new Users
                    {
                        UserName = "fakeModeratorMail@mail.com",
                        Email = "fakeModeratorMail@mail.com",
                        PhoneNumber = "0987654321",
                        Surname = "ModeratorSurname",
                        Name = "ModeratorName",
                        Patronimic = "ModeratorPatronimic",
                    };
                    await userManager.CreateAsync(moderator, "1fakeModeratorMail@mail.com");
                    await userManager.AddToRoleAsync(moderator, UserRoles.Moderator);

                    var post = new Posts
                    {
                        ProductId = product1.Id,
                        Products = product1,
                        Date = DateTime.Now,
                        Title = "Ідеальний еспресо завжди з вами: зустрічайте нашу портативну кавоварку!",
                        Users = user1,
                        UserId = user1.Id,
                        Content = "Вирушайте у свої кулінарні пригоди з нашою новою портативною кавоваркою для еспресо! 🚀 Завдяки її компактному дизайну та легкій вазі, ви зможете насолоджуватися ароматною кавою де б ви не були. Чи це подорож, похід чи відпочинок на природі, наша кавоварка стане вашим надійним супутником у створенні ідеального еспресо. ☕️✨"
                    };

                    context.Add(post);

                    context.SaveChanges();
                }
                
            }

        }
    }
}
