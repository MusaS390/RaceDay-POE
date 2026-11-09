using System.ComponentModel.DataAnnotations;
namespace RaceDay.Mvc.Models;
public class LoginVm { [Required,EmailAddress] public string Email {get;set;}=""; [Required] public string Password {get;set;}=""; }
public class RegisterVm { [Required] public string FullName {get;set;}=""; [Required,EmailAddress] public string Email {get;set;}=""; [Required,MinLength(8)] public string Password {get;set;}=""; [Required] public string Role {get;set;}="Participant"; }
public class EventVm { public int EventId {get;set;} [Required] public string Name {get;set;}=""; public string? Description {get;set;} [Required] public string Location {get;set;}=""; [Required] public DateTime EventDate {get;set;}=DateTime.Today.AddDays(30); }
public class CategoryVm { [Required] public string Name {get;set;}=""; [Range(0.1,500)] public decimal DistanceKm {get;set;}=5; [Range(1,100000)] public int MaxParticipants {get;set;}=100; }
public class ResultVm { [Required] public string FinishTime {get;set;}="00:30:00"; [Range(1,100000)] public int Position {get;set;}=1; }
public class ProfileVm { [Required] public string FullName {get;set;}=""; [Required,EmailAddress] public string Email {get;set;}=""; }
