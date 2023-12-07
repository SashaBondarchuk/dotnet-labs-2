using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Eveniet voluptatem rerum laboriosam hic consequatur occaecati.", "Pepperoni" },
                    { 2, "Enim esse accusamus rerum laudantium placeat est.", "Hawaiian Roll" },
                    { 3, "Architecto vitae consequuntur dolor deserunt perspiciatis consequuntur.", "Vegetarian Pizza" },
                    { 4, "Veniam qui saepe facere dolores occaecati molestiae.", "BBQ Chicken" },
                    { 5, "Sint sint quas ipsum temporibus cumque voluptatem.", "Margherita" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Eveniet voluptatem rerum laboriosam hic.", "Italian Sausage" },
                    { 2, "Occaecati facilis enim esse accusamus.", "Artichoke Hearts" },
                    { 3, "Laudantium placeat est qui architecto.", "Sun-Dried Tomatoes" },
                    { 4, "Consequuntur dolor deserunt perspiciatis consequuntur.", "Mozzarella Cheese" },
                    { 5, "Numquam rerum veniam qui saepe.", "Feta Cheese" },
                    { 6, "Dolores occaecati molestiae non et.", "Spinach" },
                    { 7, "Aliquid libero fuga voluptas omnis.", "Green Peppers" },
                    { 8, "Sint sint quas ipsum temporibus.", "Pepperoni" },
                    { 9, "Voluptatem omnis aut odio rerum.", "Parmesan Cheese" },
                    { 10, "Aut aliquid adipisci nemo consequuntur.", "Onions" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 16, 14, 43, 50, 724, DateTimeKind.Utc).AddTicks(7349), 2, 610.639849449805960m },
                    { 2, new DateTime(2023, 11, 12, 21, 24, 22, 965, DateTimeKind.Utc).AddTicks(1041), 1, 1421.171550136604320m },
                    { 3, new DateTime(2023, 8, 3, 1, 58, 4, 230, DateTimeKind.Utc).AddTicks(4576), 1, 1215.827997418041000m },
                    { 4, new DateTime(2023, 10, 17, 5, 25, 24, 446, DateTimeKind.Utc).AddTicks(286), 1, 482.587243347702360m },
                    { 5, new DateTime(2023, 9, 6, 15, 25, 38, 562, DateTimeKind.Utc).AddTicks(3907), 1, 1421.953232475533920m },
                    { 6, new DateTime(2023, 1, 24, 21, 19, 32, 145, DateTimeKind.Utc).AddTicks(2772), 1, 1039.94646496136920m },
                    { 7, new DateTime(2023, 9, 17, 5, 40, 58, 911, DateTimeKind.Utc).AddTicks(6102), 0, 141.4160956635215520m },
                    { 8, new DateTime(2023, 2, 10, 21, 31, 45, 626, DateTimeKind.Utc).AddTicks(306), 1, 898.43411052526560m },
                    { 9, new DateTime(2023, 10, 22, 6, 0, 55, 936, DateTimeKind.Utc).AddTicks(5001), 0, 965.708769534578240m },
                    { 10, new DateTime(2023, 10, 25, 21, 4, 35, 711, DateTimeKind.Utc).AddTicks(1932), 1, 440.086971623862960m },
                    { 11, new DateTime(2023, 6, 22, 1, 27, 43, 128, DateTimeKind.Utc).AddTicks(2041), 1, 1133.63747469784560m },
                    { 12, new DateTime(2023, 3, 21, 20, 28, 55, 436, DateTimeKind.Utc).AddTicks(9835), 1, 1390.025915228774240m },
                    { 13, new DateTime(2023, 8, 11, 2, 40, 4, 541, DateTimeKind.Utc).AddTicks(9544), 1, 335.888394618354280m },
                    { 14, new DateTime(2023, 7, 2, 21, 30, 51, 584, DateTimeKind.Utc).AddTicks(8029), 2, 947.152145638667320m },
                    { 15, new DateTime(2023, 6, 21, 10, 36, 54, 535, DateTimeKind.Utc).AddTicks(6088), 2, 239.016087045435160m }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "g" },
                    { 2, "ml" },
                    { 3, "pcs" }
                });

            migrationBuilder.InsertData(
                table: "DishIngredients",
                columns: new[] { "DishId", "IngredientId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 8 },
                    { 1, 10 },
                    { 2, 4 },
                    { 2, 9 },
                    { 2, 10 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 6 },
                    { 3, 9 },
                    { 3, 10 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 5, 3 },
                    { 5, 5 },
                    { 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "Portions",
                columns: new[] { "Id", "Amount", "Description", "DishId", "Price", "UnitId" },
                values: new object[,]
                {
                    { 1, 579.511296367045200m, "In eveniet voluptatem rerum laboriosam.", 5, 616.584757536921600m, 1 },
                    { 2, 385.80565777877600m, "Facilis enim esse accusamus rerum.", 4, 751.32241349263200m, 1 },
                    { 3, 437.41462446628800m, "Qui architecto vitae consequuntur dolor.", 3, 300.352229597211200m, 1 },
                    { 4, 557.937953415298000m, "Rerum numquam rerum veniam qui.", 4, 597.876540011668800m, 1 },
                    { 5, 340.218343464759200m, "Occaecati molestiae non et numquam.", 4, 701.163321966847200m, 1 },
                    { 6, 401.778772381031200m, "Voluptas omnis ratione sint sint.", 1, 831.138423379109600m, 1 },
                    { 7, 554.79099021982000m, "Cumque voluptatem omnis aut odio.", 2, 844.435534553804800m, 1 },
                    { 8, 409.659736701128800m, "Aliquid adipisci nemo consequuntur et.", 1, 799.290544446227200m, 1 },
                    { 9, 402.142979671314000m, "Natus et aliquid voluptatem recusandae.", 3, 387.933481199636000m, 1 },
                    { 10, 450.50262727332400m, "Voluptate blanditiis veniam tempora dignissimos.", 4, 307.63638471608800m, 1 },
                    { 11, 547.748195728169600m, "Ipsa explicabo qui architecto sed.", 1, 704.935183238673600m, 1 },
                    { 12, 417.07221074824800m, "Est est necessitatibus sit ipsa.", 3, 454.221372797256800m, 1 },
                    { 13, 259.485794445260400m, "Nihil in ea mollitia sequi.", 5, 356.403831092828800m, 1 },
                    { 14, 219.901462094812400m, "Voluptas ut voluptatum voluptatibus id.", 2, 780.446354570075200m, 1 },
                    { 15, 220.6978339798272800m, "Qui quia vitae ut earum.", 4, 752.584564570609600m, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "PortionId", "Quantity" },
                values: new object[,]
                {
                    { 1, 14, 5, 2 },
                    { 2, 6, 15, 5 },
                    { 3, 8, 10, 5 },
                    { 4, 7, 14, 2 },
                    { 5, 7, 12, 5 },
                    { 6, 9, 2, 4 },
                    { 7, 2, 12, 1 },
                    { 8, 9, 2, 3 },
                    { 9, 4, 14, 4 },
                    { 10, 6, 14, 2 },
                    { 11, 8, 8, 4 },
                    { 12, 8, 4, 5 },
                    { 13, 6, 11, 2 },
                    { 14, 13, 9, 4 },
                    { 15, 14, 8, 1 },
                    { 16, 3, 12, 3 },
                    { 17, 5, 12, 4 },
                    { 18, 7, 14, 5 },
                    { 19, 13, 6, 2 },
                    { 20, 5, 2, 1 },
                    { 21, 8, 2, 1 },
                    { 22, 9, 14, 4 },
                    { 23, 1, 15, 2 },
                    { 24, 8, 4, 3 },
                    { 25, 7, 5, 3 },
                    { 26, 7, 10, 2 },
                    { 27, 3, 1, 4 },
                    { 28, 9, 2, 1 },
                    { 29, 14, 1, 1 },
                    { 30, 10, 11, 4 },
                    { 31, 1, 1, 5 },
                    { 32, 9, 5, 3 },
                    { 33, 6, 6, 3 },
                    { 34, 3, 3, 3 },
                    { 35, 3, 13, 5 },
                    { 36, 8, 15, 3 },
                    { 37, 1, 6, 4 },
                    { 38, 9, 3, 4 },
                    { 39, 15, 15, 1 },
                    { 40, 10, 11, 1 },
                    { 41, 11, 6, 5 },
                    { 42, 10, 7, 2 },
                    { 43, 13, 14, 1 },
                    { 44, 13, 1, 5 },
                    { 45, 1, 15, 2 },
                    { 46, 4, 6, 5 },
                    { 47, 9, 6, 2 },
                    { 48, 1, 9, 1 },
                    { 49, 3, 7, 2 },
                    { 50, 7, 9, 5 },
                    { 51, 6, 12, 2 },
                    { 52, 12, 7, 4 },
                    { 53, 10, 9, 4 },
                    { 54, 6, 10, 2 },
                    { 55, 10, 4, 1 },
                    { 56, 7, 9, 2 },
                    { 57, 3, 11, 1 },
                    { 58, 13, 1, 3 },
                    { 59, 12, 11, 2 },
                    { 60, 13, 7, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "DishIngredients",
                keyColumns: new[] { "DishId", "IngredientId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Portions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
