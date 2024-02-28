using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.BLL.Services;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using FluentAssertions;
using Xunit;
using System.Threading;
using ServiceDesk.Domain.Exceptions;

namespace ServiceDesk.Tests;

public class ExecutionRequestServiceTests
{
    private static readonly IGenericRepository<Ticket> _ticketRepository;
    private static readonly IGenericRepository<User> _userRepository;
    private static readonly IGenericRepository<ExecutionRequest> _executionRequestRepository;
    private static readonly IMapper _mapper;
    private static readonly IExecutionRequestService _executionRequestService;

    static ExecutionRequestServiceTests()
    {
        _ticketRepository = Substitute.For<IGenericRepository<Ticket>>();
        _userRepository = Substitute.For<IGenericRepository<User>>();
        _executionRequestRepository = Substitute.For<IGenericRepository<ExecutionRequest>>();
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ExecutionRequest, ExecutionRequestModel>();
            cfg.CreateMap<ExecutionRequestModel, ExecutionRequest>();
        }).CreateMapper();

        _executionRequestService =
            new ExecutionRequestService(_executionRequestRepository, _ticketRepository, _userRepository, _mapper);
    }

    [Fact]
    public async Task CreateModel_ValidExecutionRequestModel_ShouldExecuteCreateModel()
    {
        //Arrange
        var executionRequestModel = Substitute.For<ExecutionRequestModel>();
        var executionRequestEntity = Substitute.For<ExecutionRequest>();
        var user = Substitute.For<User>();
        var ticket = Substitute.For<Ticket>();
        _userRepository.GetEntityByIdAsync(executionRequestModel.ExecutorId, default).Returns(user);
        _ticketRepository.GetEntityByIdAsync(executionRequestModel.TicketId, default).Returns(ticket);
        _executionRequestRepository.AddEntityAsync(Arg.Any<ExecutionRequest>(), default).Returns(executionRequestEntity);

        //Act
        var result = await _executionRequestService.CreateModelAsync(executionRequestModel, default);

        //Assert
        await _executionRequestRepository.Received().AddEntityAsync(Arg.Any<ExecutionRequest>(), default);
        result.Should().BeOfType(typeof(ExecutionRequestModel));
    }

    [Fact]
    public async Task CreateModel_InvalidUserInExecutionRequestModel_ShouldThrowException()
    {
        //Arrange
        var executionRequestModel = Substitute.For<ExecutionRequestModel>();
        var executionRequestEntity = Substitute.For<ExecutionRequest>();
        var ticket = Substitute.For<Ticket>();
        _userRepository.GetEntityByIdAsync(executionRequestModel.ExecutorId, default).ReturnsNull();
        _ticketRepository.GetEntityByIdAsync(executionRequestModel.TicketId, default).Returns(ticket);
        _executionRequestRepository.AddEntityAsync(Arg.Any<ExecutionRequest>(), default).Returns(executionRequestEntity);

        //Act
        var action = async () => await _executionRequestService.CreateModelAsync(executionRequestModel, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        Assert.Equal("Entity does not exist!", exception.Message);
    }

    [Fact]
    public async Task CreateModel_InvalidTicketInExecutionRequestModel_ShouldThrowException()
    {
        //Arrange
        var executionRequestModel = Substitute.For<ExecutionRequestModel>();
        var executionRequestEntity = Substitute.For<ExecutionRequest>();
        var user = Substitute.For<User>();
        _userRepository.GetEntityByIdAsync(executionRequestModel.ExecutorId, default).Returns(user);
        _ticketRepository.GetEntityByIdAsync(executionRequestModel.TicketId, default).ReturnsNull();
        _executionRequestRepository.AddEntityAsync(Arg.Any<ExecutionRequest>(), default).Returns(executionRequestEntity);

        //Act
        var action = async () => await _executionRequestService.CreateModelAsync(executionRequestModel, default);

        //Assert
        var exception = await Assert.ThrowsAsync<EntityIsNullException>(action);
        Assert.Equal("Entity does not exist!", exception.Message);
    }

    [Fact]
    public async Task GetExecutionRequestsByTicket_ValidTicketId_ReturnListOfExecutionRequests()
    {
        //Arrange
        var id = new Guid();
        var executionRequestEntities = Substitute.For<IEnumerable<ExecutionRequest>>();
        _executionRequestRepository.GetEntitiesByPredicateAsync(p => p.TicketId == id, default).Returns(executionRequestEntities);

        //Act
        var result = await _executionRequestService.GetExecutionRequestsByTicket(id, default);

        //Assert
        result.Should().BeOfType(typeof(List<ExecutionRequestModel>));
    }

    [Fact]
    public async Task GetExecutionRequestsByExecutor_ValidTicketId_ReturnListOfExecutionRequests()
    {
        //Arrange
        var id = new Guid();
        var executionRequestEntities = Substitute.For<IEnumerable<ExecutionRequest>>();
        _executionRequestRepository.GetEntitiesByPredicateAsync(p => p.ExecutorId == id, default).Returns(executionRequestEntities);

        //Act
        var result = await _executionRequestService.GetExecutionRequestsByTicket(id, default);

        //Assert
        result.Should().BeOfType(typeof(List<ExecutionRequestModel>));
    }
}
