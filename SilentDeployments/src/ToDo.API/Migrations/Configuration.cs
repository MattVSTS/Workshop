namespace MultiChannelToDo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MultiChannelToDo.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<MultiChannelToDo.Models.MultiChannelToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MultiChannelToDo.Models.MultiChannelToDoContext context)
        {
            context.ToDoItems.AddOrUpdate(
            new TodoItem
            {
                Id = "6DF176EA-E0D4-4C04-ABFA-8533CB4680CE",
                //URef = 1,
                Text = "Start your pipeline",
                Complete = false,
                Deleted = false,
                UpdatedAt = DateTimeOffset.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow
            },
            new TodoItem
            {
                Id = "50DF07C4-A769-4084-BA5B-F4F5FBF28688",
                //URef = 2,
                Text = "Create ARM Template",
                Complete = false,
                Deleted = false,
                UpdatedAt = DateTimeOffset.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow
            },
            new TodoItem
            {
                Id = "585A4B75-CB85-44E8-BF50-B49AB6965632",
                //URef = 3,
                Text = "Publish to Azure",
                Complete = false,
                Deleted = false,
                UpdatedAt = DateTimeOffset.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow
            },
            new TodoItem
            {
                Id = "A23A2F75-CC52-4F1A-B459-4F8AB019CB4D",
                //URef = 4,
                Text = "Release continuously",
                Complete = false,
                Deleted = false,
                UpdatedAt = DateTimeOffset.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow
            },
            new TodoItem
            {
                Id = "EF7C5452-E551-4456-985D-AB41C7B0BFFD",
                //URef = 5,
                Text = "Bring value!",
                Complete = false,
                Deleted = false,
                UpdatedAt = DateTimeOffset.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow
            });
        }
    }
}
