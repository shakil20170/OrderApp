using OrderApp.Client.Pages;
using OrderApp.Client.Pages.WindowPage;
using OrderApp.Server.Entity;
using OrderApp.Server.Repositories.Contracts;
using OrderApp.Shared.Dtos;
using System.Linq;
using System.Xml.Linq;
using static OrderApp.Shared.Dtos.WindowDto;

namespace OrderApp.Server.Extensions
{
    public static class DtoConversions
    {

        public static IEnumerable<OrderDto> ConvertToDto(this IEnumerable<OrderTable> orders)
        {
            return (from order in orders
                    select new OrderDto
                    {
                        OrderId = order.OrderId,
                        Name = order.Name,
                        State = order.State
                    });
        }
        public static OrderDto ConvertToDto(this OrderTable order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                Name = order.Name,
                State = order.State

            };
        }


        public static IEnumerable<WindowDto> ConvertToWindowDto(this IEnumerable<WindowTable> windows,IEnumerable<OrderTable> orders)
        {
            return (from window in windows
                    join order in orders on window.OrderTableOrderId equals order.OrderId
                    select new WindowDto
                    {
                        OrderId = order.OrderId,
                        Name = order.Name,
                        //State = order.State,
                        WindowID = window.WindowID,
                        WindowName = window.WindowName,
                        QuantityOfWindows = window.QuantityOfWindows,
                        TotalSubElements = window.TotalSubElements,
                        OrderTableOrderId =  window.OrderTableOrderId
                    });
        }

        public static WindowDto ConvertToSingleWindowDto(this WindowTable window,OrderTable order)
        {
            return new WindowDto
            {
                OrderId = order.OrderId,
                Name = order.Name,
                //State = order.State,
                WindowID = window.WindowID,
                WindowName = window.WindowName,
                QuantityOfWindows = window.QuantityOfWindows,
                TotalSubElements = window.TotalSubElements,
                OrderTableOrderId = window.OrderTableOrderId,
            };
        }

        public static WindowDto ConvertToSingleWindowDto(this WindowTable window)
        {
            return new WindowDto
            {
                WindowID = window.WindowID,
                WindowName = window.WindowName,
                QuantityOfWindows = window.QuantityOfWindows,
                TotalSubElements = window.TotalSubElements,
                OrderTableOrderId = window.OrderTableOrderId,

            };
        }

        public static IEnumerable<SubElementDto> ConvertToSubElementDto(this IEnumerable<SubElementTable> subElements, IEnumerable<WindowTable> windows)
        {
            return (from subElement in subElements
                    join window in windows on subElement.WindowTableWindowID equals window.WindowID
                    select new SubElementDto
                    {
                      
                        WindowName = window.WindowName,
                        SubElementID = subElement.SubElementID,
                        WindowTableWindowID = subElement.WindowTableWindowID,
                        Element = subElement.Element,
                        SubElementType = subElement.SubElementType,
                        Width = subElement.Width,
                        Height = subElement.Height
                    });
        }

        public static SubElementDto ConvertToSingleSubElelmentDto(this SubElementTable subElement,string windowName)
        {
            return new SubElementDto
            {
                WindowTableWindowID = subElement.WindowTableWindowID,
                SubElementID = subElement.SubElementID,
                Element = subElement.Element,
                SubElementType = subElement.SubElementType,
                Width = subElement.Width,
                Height = subElement.Height,
                WindowName = windowName
            };
        }



    }
}

