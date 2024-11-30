using App.Repository;
using App.Repository.Domain;
using FreeScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Extensions;

public static class FreeSqlDbContextExtensions
{

    /// <summary>
    /// 同步调度结构
    /// </summary>
    /// <param name="that"></param>
    /// <param name="dbConfig"></param>
    /// <param name="configureFreeSql"></param>
    public static void SyncSchedulerStructure(this IFreeSql that, DbConfig dbConfig, Action<IFreeSql> configureFreeSql = null)
    {
        that.CodeFirst
        .ConfigEntity<TaskInfo>(a =>
        {
            a.Name("ad_task");
            a.Property(b => b.Id).IsPrimary(true);
            a.Property(b => b.Body).StringLength(-1);
            a.Property(b => b.Interval).MapType(typeof(int));
            a.Property(b => b.IntervalArgument).StringLength(1024);
            a.Property(b => b.Status).MapType(typeof(int));
            a.Property(b => b.CreateTime).ServerTime(DateTimeKind.Local);
            a.Property(b => b.LastRunTime).ServerTime(DateTimeKind.Local);
        })
        .ConfigEntity<TaskLog>(a =>
        {
            a.Name("ad_task_log");
            a.Property(b => b.Exception).StringLength(-1);
            a.Property(b => b.Remark).StringLength(-1);
            a.Property(b => b.CreateTime).ServerTime(DateTimeKind.Local);
        })
        .ConfigEntity<TaskInfoExt>(a =>
        {
            a.Name("ad_task_ext");
            a.Property(b => b.TaskId).IsPrimary(true);
            a.Property(b => b.CreatedTime).CanUpdate(false).ServerTime(DateTimeKind.Local);
            a.Property(b => b.ModifiedTime).CanInsert(false).ServerTime(DateTimeKind.Local);
        });

        configureFreeSql?.Invoke(that);

        if (dbConfig.SyncStructure)
        {
            that.CodeFirst.SyncStructure<TaskInfo>();
            that.CodeFirst.SyncStructure<TaskLog>();
            that.CodeFirst.SyncStructure<TaskInfoExt>();
        }

    }
}
