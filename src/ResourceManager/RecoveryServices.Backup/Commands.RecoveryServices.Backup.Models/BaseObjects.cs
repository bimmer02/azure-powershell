﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.RecoveryServices.Backup.Models;

namespace Microsoft.Azure.Commands.RecoveryServices.Backup.Cmdlets.Models
{
    public class AzureRmRecoveryServicesObjectBase
    {
        public virtual void Validate() { }
    }

    public class AzureRmRecoveryServicesBackupManagementContext : AzureRmRecoveryServicesObjectBase
    {
        /// <summary>
        /// BackupManagementType
        /// </summary>
        public BackupManagementType BackupManagementType { get; set; }

        public AzureRmRecoveryServicesBackupManagementContext() { }

        public AzureRmRecoveryServicesBackupManagementContext(string backupManagementType)
        {
            BackupManagementType = ConversionUtils.GetPsBackupManagementType(backupManagementType);
        }
    }

    public class AzureRmRecoveryServicesContainerContext : AzureRmRecoveryServicesBackupManagementContext
    {
        public ContainerType ContainerType { get; set; }

        public AzureRmRecoveryServicesContainerContext() { }

        public AzureRmRecoveryServicesContainerContext(ContainerType containerType, string backupManagementType)
            : base(backupManagementType)
        {
            ContainerType = containerType;
        }
    }

    public class AzureRmRecoveryServicesContainerBase : AzureRmRecoveryServicesContainerContext
    {
        /// <summary>
        /// Container Name
        /// </summary>
        public string Name { get; set; }

        public AzureRmRecoveryServicesContainerBase(ProtectionContainerResource protectionContainer)
            : base(ConversionUtils.GetPsContainerType(((ProtectionContainer)protectionContainer.Properties).ContainerType), 
                   ((ProtectionContainer)protectionContainer.Properties).BackupManagementType)
        {
            Name = protectionContainer.Name;
        }
    }

    /// <summary>
    /// Represents Azure Backup Item Context Class
    /// </summary>
    public class AzureRmRecoveryServicesItemContext : AzureRmRecoveryServicesContainerContext
    {
        /// <summary>
        /// Workload Type of Item
        /// </summary>
        public WorkloadType WorkloadType { get; set; }

        /// <summary>
        /// Unique name of the Container
        /// </summary>
        public string ContainerName { get; set; }

        public AzureRmRecoveryServicesItemContext()
            : base()
        {

        }

        public AzureRmRecoveryServicesItemContext(ProtectedItem protectedItem,
            AzureRmRecoveryServicesContainerBase container)
            : base(container.ContainerType, protectedItem.BackupManagementType)
        {
            WorkloadType = ConversionUtils.GetPsWorkloadType(protectedItem.WorkloadType);
            ContainerName = protectedItem.ContainerName;
        }
    }

    /// <summary>
    /// Represents Azure Backup Item Base Class
    /// </summary>
    public class AzureRmRecoveryServicesItemBase : AzureRmRecoveryServicesItemContext
    {
        /// <summary>
        /// Last Recovery Point for the item
        /// </summary>
        public DateTime? LastRecoveryPoint { get; set; }

        public AzureRmRecoveryServicesItemBase(ProtectedItem protectedItem,
            AzureRmRecoveryServicesContainerBase container)
            : base(protectedItem, container)
        {
            LastRecoveryPoint = protectedItem.LastRecoveryPoint;
        }
    }

    /// <summary>
    /// Represents Azure Backup Item ExtendedInfo Base Class
    /// </summary>
    public class AzureRmRecoveryServicesItemExtendedInfoBase : AzureRmRecoveryServicesObjectBase
    {
    }

    public class AzureRmRecoveryServicesRecoveryPointBase : AzureRmRecoveryServicesItemContext
    {
        private global::Microsoft.Azure.Management.RecoveryServices.Backup.Models.RecoveryPointResource rp;

        /// <summary>
        ///Type of recovery point (appConsistent\CrashConsistent etc) 
        /// </summary>
        ///
        public String RecoveryPointType { get; set; }

        /// <summary>
        /// Time of RecoveryPoint
        /// </summary>
        public DateTime RecoveryPointTime { get; set; }

        public AzureRmRecoveryServicesRecoveryPointBase()
            : base()
        {
        }
    }


    public class AzureRmRecoveryServicesPolicyBase : AzureRmRecoveryServicesBackupManagementContext
    {
        public string Name { get; set; }

        public WorkloadType WorkloadType { get; set; }

        public override void Validate()
        {
        }
    }

    public class AzureRmRecoveryServicesRetentionPolicyBase : AzureRmRecoveryServicesObjectBase
    {
        public override void Validate()
        {
        }
    }

    public class AzureRmRecoveryServicesSchedulePolicyBase : AzureRmRecoveryServicesObjectBase
    {
        public override void Validate()
        {
        }
    }

    public class AzureRmRecoveryServicesJobBase : AzureRmRecoveryServicesBackupManagementContext
    {
        public string ActivityId { get; set; }

        public string InstanceId { get; set; }

        public string Operation { get; set; }

        public string Status { get; set; }

        public string WorkloadName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }

    /// <summary>
    /// This class is does not represent first class resource. So, we are not inheriting from the base class.
    /// </summary>
    public class AzureRmRecoveryServicesJobErrorInfoBase
    {
        public string ErrorMessage { get; set; }

        public List<string> Recommendations { get; set; }
    }

    /// <summary>
    /// This class is does not represent a first class resource. So, we are not inheriting from the common base class.
    /// </summary>
    public class AzureRmRecoveryServicesJobSubTaskBase
    {
        public string Name { get; set; }

        public string Status { get; set; }
    }
}
