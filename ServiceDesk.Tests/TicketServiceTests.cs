using System.Linq.Expressions;
using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Services;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ServiceDesk.BLL.Mapping;
using ServiceDesk.BLL.Models;
using ServiceDesk.Domain.Enums;
using ServiceDesk.Domain.Exceptions;
using Xunit;

namespace ServiceDesk.Tests;

public class TicketServiceTests
{
    private static readonly IGenericRepository<Ticket> _ticketRepository;
    private static readonly IGenericRepository<User> _userRepository;
<<<<<<< HEAD
    private static readonly IMapper _mapper;//сущю конфиг.
=======
    private static readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
>>>>>>> 81e208b (fix tests for ticket service)
    private static readonly ITicketService _ticketService;

    static TicketServiceTests()
    {
        _ticketRepository = Substitute.For<IGenericRepository<Ticket>>();
        _userRepository = Substitute.For<IGenericRepository<User>>();

        _ticketService = new TicketService(_ticketRepository, _userRepository, _mapper);
    }

    [Fact]
    public async Task GetModelById_ValidId_ReturnModel()
    {
        //Arrange
        var id = new Guid();
        var ticket = new Ticket
        {
            Name = "Test name"
        };

        _ticketRepository.GetEntityByIdAsync(id, default).Returns(ticket);

        //Act
        var result = await _ticketService.GetModelByIdAsync(id, default);

        //Assert
        result.Should().NotBeNull();
        Assert.Equal("Test name", result.Name);
    }

    [Fact]
    public async Task GetModelById_InvalidId_ReturnNull()
    {
        //Arrange
        var id = new Guid();
        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        var result = await _ticketService.GetModelByIdAsync(id, default);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTicketStatus_ValidId_ReturnStatus()
    {
        //Arrange
        var id = new Guid();
        var ticket = new Ticket
        {
            Status = Status.InProgress
        };

        _ticketRepository.GetEntityByIdAsync(id, default).Returns(ticket);

        //Act
        var result = await _ticketService.GetTicketStatus(id, default);

        //Assert
        result.Should().Be(Status.InProgress);
    }

    [Fact]
    public async Task GetTicketStatus_InvalidId_ThrowException()
    {
        //Arrange
        var id = new Guid();
        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        var action = async () => await _ticketService.GetTicketStatus(id, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        Assert.Equal("Entity does not exist!", exception.Message);
    }

    [Fact]
    public async Task GetAll_WhiteData_ReturnListOfModels()
    {
        //Arrange
<<<<<<< HEAD
        var entities = new List<Ticket>();
        var models = new List<TicketModel>();
        _ticketRepository.GetAllAsync(default).Returns(entities);
=======
        var tickets = new List<Ticket>
        {
            new ()
            {
                Name = "Test"
            }
        };

        var models = new List<TicketModel>
        {
            new ()
            {
                Name = "Test"
            }
        };

        _ticketRepository.GetAllAsync(default).Returns(tickets);
>>>>>>> 81e208b (fix tests for ticket service)

        //Act
        var result = await _ticketService.GetAllAsync(default);

        //Assert
<<<<<<< HEAD
        Assert.Equal(models, result);
=======
        Assert.Equivalent(models, result);
>>>>>>> 81e208b (fix tests for ticket service)
    }

    [Fact]
    public async Task GetListByPredicate_WhiteData_ReturnListOfModels()
    {
        //Arrange
<<<<<<< HEAD
        var entities = new List<Ticket>();
        var models = new List<TicketModel>();
=======
        var tickets = new List<Ticket>
        {
            new ()
            {
                Name = "Test"
            }
        };

        var models = new List<TicketModel>
        {
            new ()
            {
                Name = "Test"
            }
        };

>>>>>>> 81e208b (fix tests for ticket service)
        var predicate = Arg.Any<Expression<Func<Ticket, bool>>>();

        _ticketRepository.GetEntitiesByPredicateAsync(predicate, default).Returns(tickets);

        //Act
        var result = await _ticketService.GetListByPredicateAsync(predicate, default);

        //Assert
<<<<<<< HEAD
        Assert.Equal(models, result);
=======
        Assert.Equivalent(models, result);
>>>>>>> 81e208b (fix tests for ticket service)
    }

    [Fact]
    public async Task CreateModel_ValidTicketModel_ShouldExecuteCreateModel()
    {
        //Arrange
        var ticketModel = new TicketModel();
        var ticketEntity = new Ticket
        {
            Name = "Test name"
        };

        var userModel = new User();
        _userRepository.GetEntityByIdAsync(ticketModel.UserId, default).Returns(userModel);
        _ticketRepository.AddEntityAsync(Arg.Any<Ticket>(), default).Returns(ticketEntity);

        //Act
        var result = await _ticketService.CreateModelAsync(ticketModel, default);

        //Assert
        Assert.Equal("Test name", result.Name);
    }

    [Fact]
    public async Task CreateModel_InvalidUserInTicketModel_ShouldThrowException()
    {
        //Arrange
        var ticketModel = new TicketModel();
        var ticketEntity = new Ticket();
        _userRepository.GetEntityByIdAsync(ticketModel.UserId, default).ReturnsNull();
        _ticketRepository.AddEntityAsync(Arg.Any<Ticket>(), default).Returns(ticketEntity);

        //Act
        var action = async () => await _ticketService.CreateModelAsync(ticketModel, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        Assert.Equal("Entity does not exist!", exception.Message);
    }

    [Fact]
    public async Task DeleteModel_ValidId_ShouldExecuteDeleteModel()
    {
        //Arrange
        var id = new Guid();
        var entity = new Ticket();
        _ticketRepository.GetEntityByIdAsync(id, default).Returns(entity);

        //Act
        await _ticketService.DeleteModelAsync(id, default);

        //Assert
        await _ticketRepository.Received().DeleteEntityAsync(entity, default);
    }

    [Fact]
    public async Task DeleteModel_InvalidId_ShouldNotExecuteDeleteModel()
    {
        //Arrange
        var id = new Guid();
        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        await _ticketService.DeleteModelAsync(id, default);

        //Assert
        await _ticketRepository.DidNotReceive().DeleteEntityAsync(Arg.Any<Ticket>(), default);
    }

    [Fact]
    public async Task UpdateModel_InvalidModel_ShouldThrowException()
    {
        //Arrange
        var id = new Guid();
        var ticketModel = new TicketModel();

        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        var action = async () => await _ticketService.UpdateModelAsync(id, ticketModel, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        Assert.Equal("Entity does not exist!", exception.Message);
    }

    [Fact]
    public async Task UpdateModel_ValidModel_ShouldExecuteUpdateModel() //
    {
        //Arrange
        var id = new Guid();
        var ticket = new Ticket();

        var newTicket = new Ticket
        {
            Name = "Test name is changed"
        };

        var model = new TicketModel
        {
            Name = "Test name is changed"
        };

        _ticketRepository.GetEntityByIdAsync(id, default).Returns(ticket);
        _ticketRepository.UpdateEntityAsync(Arg.Any<Ticket>(), default).Returns(newTicket);

        //Act
        var result = await _ticketService.UpdateModelAsync(id, model, default);

        //Assert
        Assert.Equal(model.Name, result.Name);
    }

    [Fact]
    public async Task SetTicketStatus_InvalidModel_ShouldThrowException()
    {
        //Arrange
        var id = new Guid();
        var status = Status.None;
        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        var action = async () => await _ticketService.SetTicketStatus(id, status, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        Assert.Equal("Entity does not exist!", exception.Message);
    }

    [Fact]
    public async Task SetTicketStatus_ValidModel_ShouldExecuteUpdateModel()
    {
        //Arrange
        var id = new Guid();
        var ticket = new Ticket();
        var status = Status.Free;
        _ticketRepository.GetEntityByIdAsync(id, default).Returns(ticket);

        //Act
        var result = await _ticketService.SetTicketStatus(id, status, default);

        //Assert
        Assert.Equal(status, result.Status);
    }

    [Fact]
    public async Task GetTicketsByUser_ValidId_ReturnListOfTickets()
    {
        //Arrange
        var id = new Guid();
        var tickets = new List<Ticket>
        {
<<<<<<< HEAD
            new Ticket()
=======
            new ()
>>>>>>> 81e208b (fix tests for ticket service)
            {
                UserId = id
            }
        };

        var models = new List<TicketModel>
        {
            new ()
            {
                UserId = id
            }
        };

<<<<<<< HEAD
        _ticketRepository.GetEntitiesByPredicateAsync(p => p.UserId == id, default).Returns(tickets);
=======
        var predicate = Arg.Any<Expression<Func<Ticket, bool>>>();
        _ticketRepository.GetEntitiesByPredicateAsync(predicate, default).Returns(tickets);
>>>>>>> 81e208b (fix tests for ticket service)

        //Act
        var result = await _ticketService.GetTicketsByUser(id, default);

        //Assert
<<<<<<< HEAD
        Assert.Equal(models, result);
=======
        Assert.Equivalent(models, result);
>>>>>>> 81e208b (fix tests for ticket service)
    }
}