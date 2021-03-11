﻿using NUnit.Framework;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Exceptions;
using BerkeGaming.Application.TodoLists.Commands.CreateTodoList;
using BerkeGaming.Application.TodoLists.Commands.DeleteTodoList;
using BerkeGaming.Domain.Entities;
using FluentAssertions;

namespace CleanArchitecture.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class DeleteTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoListId()
        {
            var command = new DeleteTodoListCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoList()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            await SendAsync(new DeleteTodoListCommand 
            { 
                Id = listId 
            });

            var list = await FindAsync<TodoList>(listId);

            list.Should().BeNull();
        }
    }
}
