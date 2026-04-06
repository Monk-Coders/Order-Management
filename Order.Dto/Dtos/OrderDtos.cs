using System;
using System.Collections.Generic;

namespace Order.Services.Dtos
{
    public class OrderDto
    {
        public int OrdId { get; set; }
        public Guid OrdGuid { get; set; }
        public Guid OrdCustomerId { get; set; }
        public DateTime OrdOrderedDtt { get; set; }
        public decimal OrdTotalAmount { get; set; }
        public int OrdItemCount { get; set; }
        public Guid? OrdStatus { get; set; }
        public bool OrdIsActive { get; set; }
        public bool OrdIsDeleted { get; set; }
        public string? OrdCreatedBy { get; set; }
        public DateTime OrdCreatedDtt { get; set; }
        public string? OrdModifiedBy { get; set; }
        public DateTime OrdModifiedDtt { get; set; }

        // Navigation
        public List<OrderItemDto>? Items { get; set; }
    }

    public class OrderItemDto
    {
        public int OitId { get; set; }
        public Guid OitGuid { get; set; }
        public Guid OitOrderGuid { get; set; }
        public Guid OitProductId { get; set; }
        public int OitQuantity { get; set; }
        public decimal OitUnitPrice { get; set; }
        public decimal OitTotalPrice { get; set; }
        public bool OitIsActive { get; set; }
        public bool OitIsDeleted { get; set; }
        public string? OitCreatedBy { get; set; }
        public DateTime OitCreatedDtt { get; set; }
        public string? OitModifiedBy { get; set; }
        public DateTime OitModifiedDtt { get; set; }
    }

    public class OrderStatusDto 
    {
        public int OstId { get; set; }
        public Guid OstGuid { get; set; }
        public string OstName { get; set; } = string.Empty;
        public string? OstDescription { get; set; }
        public bool OstIsActive { get; set; }
        public bool OstIsDeleted { get; set; }
        public string? OstCreatedBy { get; set; }
        public DateTime OstCreatedDtt { get; set; }
        public string? OstModifiedBy { get; set; }
        public DateTime OstModifiedDtt { get; set; }
    }

    public class ProductDto
    {
        public int PrdId { get; set; }
        public Guid PrdGuid { get; set; }
        public string PrdName { get; set; } = string.Empty;
        public string? PrdDescription { get; set; }
        public string PrdCode { get; set; } = string.Empty;
        public decimal PrdPrice { get; set; }
        public bool PrdIsActive { get; set; }
        public bool PrdIsDeleted { get; set; }
        public string? PrdCreatedBy { get; set; }
        public DateTime PrdCreatedDtt { get; set; }
        public string? PrdModifiedBy { get; set; }
        public DateTime PrdModifiedDtt { get; set; }
    }

    public class ProductPricingDto
    {
        public int PpId { get; set; }
        public Guid PpGuid { get; set; }
        public Guid PpProductGuid { get; set; }
        public decimal PpPrice { get; set; }
        public DateTime PpEffectiveDate { get; set; }
        public DateTime? PpExpiryDate { get; set; }
        public bool PpIsActive { get; set; }
        public bool PpIsDeleted { get; set; }
        public string? PpCreatedBy { get; set; }
        public DateTime PpCreatedDtt { get; set; }
        public string? PpModifiedBy { get; set; }
        public DateTime PpModifiedDtt { get; set; }
    }

    public class CustomerDto
    {
        public int CstId { get; set; }
        public Guid CstGuid { get; set; }
        public string CstName { get; set; } = string.Empty;
        public string CstEmail { get; set; } = string.Empty;
        public string? CstAddress { get; set; }
        public bool CstIsActive { get; set; }
        public bool CstIsDeleted { get; set; }
        public string? CstCreatedBy { get; set; }
        public DateTime CstCreatedDtt { get; set; }
        public string? CstModifiedBy { get; set; }
        public DateTime CstModifiedDtt { get; set; }
    }

    public class InvoiceDto
    {
        public int InvId { get; set; }
        public Guid InvGuid { get; set; }
        public Guid InvOrderGuid { get; set; }
        public string InvInvoiceNumber { get; set; } = string.Empty;
        public DateTime InvInvoiceDate { get; set; }
        public string? InvPaymentMethod { get; set; }
        public string? InvBillingAddress { get; set; }
        public string? InvShippingAddress { get; set; }
        public decimal InvDiscountAmount { get; set; }
        public decimal InvTotalAmount { get; set; }
        public DateTime? InvDueDate { get; set; }
        public bool InvIsActive { get; set; }
        public bool InvIsDeleted { get; set; }
        public string? InvCreatedBy { get; set; }
        public DateTime InvCreatedDtt { get; set; }
        public string? InvModifiedBy { get; set; }
        public DateTime InvModifiedDtt { get; set; }
    }
}