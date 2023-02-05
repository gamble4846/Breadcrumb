using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Breadcrumb.Model
{
    public class AccessModel
    {
        [Required]
public string GUIDAccess{get;set;}
[Required]
public string ID{get;set;}
public string Password{get;set;}
public string Name{get;set;}
public string EMail{get;set;}
public string EmailSignature{get;set;}
public string BCCAddress{get;set;}
public string MessageAddr{get;set;}
[Required]
public bool Active{get;set;}
public string GUIDSalesperson{get;set;}
[Required]
public bool AutoOpenAlerts{get;set;}
[Required]
public bool AutoOpenActivities{get;set;}
[Required]
public bool AutoOpenSchedule{get;set;}
public string DefaultActivityType{get;set;}
public string DefaultScheduleClass{get;set;}
[Required]
public bool AutoOpenDashboard{get;set;}
[Required]
public bool AutoOpenOrderManager{get;set;}
[Required]
public bool RestrictBySalesperson{get;set;}
[Required]
public bool EmailSettingsOverride{get;set;}
public string SMTPServer{get;set;}
public string SMTPUserName{get;set;}
public string SMTPPassword{get;set;}
public int? EmailType{get;set;}
[Range(int.MinValue, int.MaxValue)]
public int SMTPEncryption{get;set;}
[Required]
public bool SMTPAuthRequired{get;set;}
public int? PrevWhatsNew{get;set;}
public int? LastWhatsNew{get;set;}
    }
}

