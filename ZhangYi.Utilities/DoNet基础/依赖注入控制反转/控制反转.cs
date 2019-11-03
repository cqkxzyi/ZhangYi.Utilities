/*
*┌────────────────────────────────────────────────┐
*│　描    述：控制反转                                                    
*│　作    者：张毅                                              
*│　版    本：1.0                                              
*│　创建时间：2019/9/21 11:07:26                        
*└────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNet基础.依赖注入控制反转
{
    /// <summary>
    /// 创建一个接口为了实现抽象
    /// </summary>
    public interface IEvent
    {
        void LoadEventDetail();
    }

    /// <summary>
    /// 实现 IEvent
    /// </summary>
    public class TechEvent : IEvent
    {
        public void LoadEventDetail()
        {
            Console.WriteLine("Technology Event Details");
        }
    }

    /// <summary>
    /// 实现 IEvent
    /// </summary>
    public class FootballEvent : IEvent
    {
        public void LoadEventDetail()
        {
            Console.WriteLine("Football Event Details");
        }
    }

    /// <summary>
    /// 实现 IEvent
    /// </summary>
    public class PartyEvent : IEvent
    {
        public void LoadEventDetail()
        {
            Console.WriteLine("Party Event Details");
        }
    }

    /// <summary>
    /// 控制反转
    /// </summary>
    public class College
    {
        private IEvent _events = null;

        /// <summary>
        /// College 构造器
        /// </summary>
        /// <param name="ie"></param>
        public College(IEvent ie)
        {
            _events = ie;
        }

        /// <summary>
        /// 方法注入
        /// </summary>
        /// <param name="myevent"></param>
        public void SetEvent(IEvent myevent)
        {
            _events = myevent;

        }
        /// <summary>
        /// 属性注入
        /// </summary>
        public IEvent MyEvent
        {
            set
            {
                _events = value;
            }
        }

        public void GetEvents()
        {
            _events.LoadEventDetail();
        }
    }

    /// <summary>
    /// 依赖注入
    /// </summary>
    public class Test
    {
        public void Test1()
        {
            College coll = new College(new FootballEvent());
            coll.GetEvents();
            coll.MyEvent = new TechEvent();
        }

    }




}
