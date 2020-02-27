using System;
using System.ComponentModel.DataAnnotations;
using Automatonymous;

namespace GDStore.DAL.Interface.Domain
{
    public class AlterationSaga : SagaStateMachineInstance
    {
        [Key]
        [Required]
        public Guid CorrelationId { get; set; }
        [Required]
        public AlterationStatus CurrentStatus { get; set; }
        [Required]
        public string CurrentState { get;set; }
        [Required]
        public string AlterationId { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}