using System;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Core.Strings;
using Umbraco.Web.PublishedCache;

namespace Umbraco.Web.PropertyEditors
{
    [DataEditor(
        Constants.PropertyEditors.Aliases.MultiUrlPicker,
        EditorType.PropertyValue,
        "Multi Url Picker",
        "multiurlpicker",
        ValueType = ValueTypes.Json,
        Group = Constants.PropertyEditors.Groups.Pickers,
        Icon = "icon-link")]
    public class MultiUrlPickerPropertyEditor : DataEditor
    {
        private readonly Lazy<IEntityService> _entityService;
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;
        private readonly IIOHelper _ioHelper;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public MultiUrlPickerPropertyEditor(ILogger logger, Lazy<IEntityService> entityService, IPublishedSnapshotAccessor publishedSnapshotAccessor, IDataTypeService dataTypeService, ILocalizationService localizationService, ILocalizedTextService localizedTextService, IIOHelper ioHelper, IShortStringHelper shortStringHelper, IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, dataTypeService, localizationService, localizedTextService, shortStringHelper, EditorType.PropertyValue)
        {
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
            _publishedSnapshotAccessor = publishedSnapshotAccessor ?? throw new ArgumentNullException(nameof(publishedSnapshotAccessor));
            _ioHelper = ioHelper;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        protected override IConfigurationEditor CreateConfigurationEditor() => new MultiUrlPickerConfigurationEditor(_ioHelper);

        protected override IDataValueEditor CreateValueEditor() => new MultiUrlPickerValueEditor(_entityService.Value, _publishedSnapshotAccessor, Logger, DataTypeService, LocalizationService, LocalizedTextService, ShortStringHelper, Attribute, _umbracoContextAccessor);
    }
}