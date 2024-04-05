using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.BLL.Services;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using ServiceDesk.BLL.Mapping;
using Xunit;
using ServiceDesk.Domain.Exceptions;
using System.Linq.Expressions;
using FluentAssertions;

namespace ServiceDesk.Tests;

public class ExecutionRequestServiceTests
{
    private static readonly IGenericRepository<Ticket> _ticketRepository;
    private static readonly IGenericRepository<User> _userRepository;
    private static readonly IGenericRepository<ExecutionRequest> _executionRequestRepository;
    private static readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
    private static readonly IExecutionRequestService _executionRequestService;

    static ExecutionRequestServiceTests()
    {
        _ticketRepository = Substitute.For<IGenericRepository<Ticket>>();
        _userRepository = Substitute.For<IGenericRepository<User>>();
        _executionRequestRepository = Substitute.For<IGenericRepository<ExecutionRequest>>();
        _executionRequestService =
            new ExecutionRequestService(_executionRequestRepository, _ticketRepository, _userRepository, _mapper);
    }

    [Fact]
    public async Task CreateModel_ValidExecutionRequestModel_ShouldExecuteCreateModel()
    {
        //Arrange
        var executionRequestModel = new ExecutionRequestModel
        {
            TicketId = new Guid()
        };

        var executionRequestEntity = new ExecutionRequest();
        var user = new User();
        var ticket = new Ticket();
        _userRepository.GetEntityByIdAsync(executionRequestModel.ExecutorId, default).Returns(user);
        _ticketRepository.GetEntityByIdAsync(executionRequestModel.TicketId, default).Returns(ticket);
        _executionRequestRepository.AddEntityAsync(Arg.Any<ExecutionRequest>(), default).Returns(executionRequestEntity);

        //Act
        var result = await _executionRequestService.CreateModelAsync(executionRequestModel, default);

        //Assert
        result.TicketId.Should().Be(executionRequestEntity.TicketId);
    }

    [Fact]
    public async Task CreateModel_InvalidUserInExecutionRequestModel_ShouldThrowException()
    {
        //Arrange
        var executionRequestModel = new ExecutionRequestModel();
        var executionRequestEntity = new ExecutionRequest();
        var ticket = new Ticket();
        _userRepository.GetEntityByIdAsync(executionRequestModel.ExecutorId, default).ReturnsNull();
        _ticketRepository.GetEntityByIdAsync(executionRequestModel.TicketId, default).Returns(ticket);
        _executionRequestRepository.AddEntityAsync(Arg.Any<ExecutionRequest>(), default).Returns(executionRequestEntity);

        //Act
        var action = async () => await _executionRequestService.CreateModelAsync(executionRequestModel, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        exception.Message.Should().Be("Entity does not exist!");
    }

    [Fact]
    public async Task CreateModel_InvalidTicketInExecutionRequestModel_ShouldThrowException()
    {
        //Arrange
        var executionRequestModel = new ExecutionRequestModel();
        var executionRequestEntity = new ExecutionRequest();
        var user = new User();
        _userRepository.GetEntityByIdAsync(executionRequestModel.ExecutorId, default).Returns(user);
        _ticketRepository.GetEntityByIdAsync(executionRequestModel.TicketId, default).ReturnsNull();
        _executionRequestRepository.AddEntityAsync(Arg.Any<ExecutionRequest>(), default).Returns(executionRequestEntity);

        //Act
        var action = async () => await _executionRequestService.CreateModelAsync(executionRequestModel, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        exception.Message.Should().Be("Entity does not exist!");
    }

    [Fact]
    public async Task GetExecutionRequestsByTicket_ValidTicketId_ReturnListOfExecutionRequests()
    {
        //Arrange
        var id = new Guid();
        var executionRequestEntities = new List<ExecutionRequest>
        {
            new ()
            {
                ExecutorId = id
            }
        };
        var executionRequestModels = new List<ExecutionRequestModel>
        {
            new ()
            {
                ExecutorId = id
            }
        };

        var predicate = Arg.Any<Expression<Func<ExecutionRequest, bool>>>();
        _executionRequestRepository.GetEntitiesByPredicateAsync(predicate, default).Returns(executionRequestEntities);

        //Act
        var result = await _executionRequestService.GetExecutionRequestsByTicket(id, default);

        //Assert
        result.Should().BeEquivalentTo(executionRequestModels);
    }

    [Fact]
    public async Task GetExecutionRequestsByExecutor_ValidTicketId_ReturnListOfExecutionRequests()
    {
        //Arrange
        var id = new Guid();
        var executionRequestEntities = new List<ExecutionRequest>
        {
            new ()
            {
                ExecutorId = id
            }
        };
        var executionRequestModels = new List<ExecutionRequestModel>
        {
            new ()
            {
                ExecutorId = id
            }
        };

        var predicate = Arg.Any<Expression<Func<ExecutionRequest, bool>>>();
        _executionRequestRepository.GetEntitiesByPredicateAsync(predicate, default).Returns(executionRequestEntities);

        //Act
        var result = await _executionRequestService.GetExecutionRequestsByTicket(id, default);

        //Assert
        result.Should().BeEquivalentTo(executionRequestModels);
    }
}
