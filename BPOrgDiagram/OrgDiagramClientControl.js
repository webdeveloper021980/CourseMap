/**
 * Basic Primitives ASP.NET BPOrgDiagram v1.0.39
 *
 * (c) Basic Primitives Inc
 *
 *
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 *
 */

/// <reference name="MicrosoftAjax.js"/>


Type.registerNamespace("BasicPrimitives.OrgDiagram");

BasicPrimitives.OrgDiagram.OrgDiagramClientControl = function (element)
{
    BasicPrimitives.OrgDiagram.OrgDiagramClientControl.initializeBase(this, [element]);
}

BasicPrimitives.OrgDiagram.OrgDiagramClientControl.prototype = {
	m_icons: ['ui-icon-carat-1-n', 'ui-icon-carat-1-ne', 'ui-icon-carat-1-e', 'ui-icon-carat-1-se', 'ui-icon-carat-1-s',
		'ui-icon-carat-1-sw', 'ui-icon-carat-1-w', 'ui-icon-carat-1-nw', 'ui-icon-carat-2-n-s', 'ui-icon-carat-2-e-w',
		'ui-icon-triangle-1-n', 'ui-icon-triangle-1-ne', 'ui-icon-triangle-1-e', 'ui-icon-triangle-1-se', 'ui-icon-triangle-1-s',
		'ui-icon-triangle-1-sw', 'ui-icon-triangle-1-w', 'ui-icon-triangle-1-nw', 'ui-icon-triangle-2-n-s', 'ui-icon-triangle-2-e-w',
		'ui-icon-arrow-1-n', 'ui-icon-arrow-1-ne', 'ui-icon-arrow-1-e', 'ui-icon-arrow-1-se', 'ui-icon-arrow-1-s', 'ui-icon-arrow-1-sw',
		'ui-icon-arrow-1-w', 'ui-icon-arrow-1-nw', 'ui-icon-arrow-2-n-s', 'ui-icon-arrow-2-ne-sw', 'ui-icon-arrow-2-e-w', 'ui-icon-arrow-2-se-nw',
		'ui-icon-arrowstop-1-n', 'ui-icon-arrowstop-1-e', 'ui-icon-arrowstop-1-s', 'ui-icon-arrowstop-1-w', 'ui-icon-arrowthick-1-n', 'ui-icon-arrowthick-1-ne',
		'ui-icon-arrowthick-1-e', 'ui-icon-arrowthick-1-se', 'ui-icon-arrowthick-1-s', 'ui-icon-arrowthick-1-sw', 'ui-icon-arrowthick-1-w',
		'ui-icon-arrowthick-1-nw', 'ui-icon-arrowthick-2-n-s', 'ui-icon-arrowthick-2-ne-sw', 'ui-icon-arrowthick-2-e-w', 'ui-icon-arrowthick-2-se-nw',
		'ui-icon-arrowthickstop-1-n', 'ui-icon-arrowthickstop-1-e', 'ui-icon-arrowthickstop-1-s', 'ui-icon-arrowthickstop-1-w', 'ui-icon-arrowreturnthick-1-w',
		'ui-icon-arrowreturnthick-1-n', 'ui-icon-arrowreturnthick-1-e', 'ui-icon-arrowreturnthick-1-s', 'ui-icon-arrowreturn-1-w', 'ui-icon-arrowreturn-1-n',
		'ui-icon-arrowreturn-1-e', 'ui-icon-arrowreturn-1-s', 'ui-icon-arrowrefresh-1-w', 'ui-icon-arrowrefresh-1-n', 'ui-icon-arrowrefresh-1-e',
		'ui-icon-arrowrefresh-1-s', 'ui-icon-arrow-4', 'ui-icon-arrow-4-diag', 'ui-icon-extlink', 'ui-icon-newwin', 'ui-icon-refresh',
		'ui-icon-shuffle', 'ui-icon-transfer-e-w', 'ui-icon-transferthick-e-w', 'ui-icon-folder-collapsed', 'ui-icon-folder-open',
		'ui-icon-document', 'ui-icon-document-b', 'ui-icon-note', 'ui-icon-mail-closed', 'ui-icon-mail-open', 'ui-icon-suitcase', 
		'ui-icon-comment', 'ui-icon-person', 'ui-icon-print', 'ui-icon-trash', 'ui-icon-locked', 'ui-icon-unlocked', 'ui-icon-bookmark',
		'ui-icon-tag', 'ui-icon-home', 'ui-icon-flag', 'ui-icon-calculator', 'ui-icon-cart', 'ui-icon-pencil', 'ui-icon-clock', 'ui-icon-disk',
		'ui-icon-calendar', 'ui-icon-zoomin', 'ui-icon-zoomout', 'ui-icon-search', 'ui-icon-wrench', 'ui-icon-gear', 'ui-icon-heart', 'ui-icon-star',
		'ui-icon-link', 'ui-icon-cancel', 'ui-icon-plus', 'ui-icon-plusthick', 'ui-icon-minus', 'ui-icon-minusthick', 'ui-icon-close', 'ui-icon-closethick',
		'ui-icon-key', 'ui-icon-lightbulb', 'ui-icon-scissors', 'ui-icon-clipboard', 'ui-icon-copy', 'ui-icon-contact', 'ui-icon-image', 'ui-icon-video',
		'ui-icon-alert', 'ui-icon-info', 'ui-icon-notice', 'ui-icon-help', 'ui-icon-check', 'ui-icon-bullet', 'ui-icon-radio-off', 'ui-icon-radio-on',
		'ui-icon-play', 'ui-icon-pause', 'ui-icon-seek-next', 'ui-icon-seek-prev', 'ui-icon-seek-end', 'ui-icon-seek-first', 'ui-icon-stop',
		'ui-icon-eject', 'ui-icon-volume-off', 'ui-icon-volume-on', 'ui-icon-power', 'ui-icon-signal-diag', 'ui-icon-signal', 'ui-icon-battery-0',
		'ui-icon-battery-1', 'ui-icon-battery-2', 'ui-icon-battery-3', 'ui-icon-circle-plus', 'ui-icon-circle-minus', 'ui-icon-circle-close',
		'ui-icon-circle-triangle-e', 'ui-icon-circle-triangle-s', 'ui-icon-circle-triangle-w', 'ui-icon-circle-triangle-n', 'ui-icon-circle-arrow-e',
		'ui-icon-circle-arrow-s', 'ui-icon-circle-arrow-w', 'ui-icon-circle-arrow-n', 'ui-icon-circle-zoomin', 'ui-icon-circle-zoomout',
		'ui-icon-circle-check', 'ui-icon-circlesmall-plus', 'ui-icon-circlesmall-minus', 'ui-icon-circlesmall-close', 'ui-icon-squaresmall-plus',
		'ui-icon-squaresmall-minus', 'ui-icon-squaresmall-close', 'ui-icon-grip-dotted-vertical', 'ui-icon-grip-dotted-horizontal', 'ui-icon-grip-solid-vertical',
		'ui-icon-grip-solid-horizontal', 'ui-icon-gripsmall-diagonal-se', 'ui-icon-grip-diagonal-se'],
    m_timer: null,
    initialize: function ()
    {
        BasicPrimitives.OrgDiagram.OrgDiagramClientControl.callBaseMethod(this, 'initialize');

        var self = this;
        var element = jQuery(self.get_element());
        var options = new primitives.orgdiagram.Config();
        var rootItem = new primitives.orgdiagram.ItemConfig();
        rootItem.itemType = primitives.orgdiagram.ItemType.Invisible;

        self._readWidgetParameters(element, options);
        self.counter = 0;
        self._recursiveReadTreeItems(rootItem, options.parameters.Items, options)

        options.rootItem = rootItem;

        options.onButtonClick = function (e, data) { self._onButtonClick(e, data); };
        options.onCursorChanging = function (e, data) { self._onCursorChanging(e, data); };
        options.onSelectionChanged = function (e, data) { self._onSelectionChanged(e, data); };

        if (BasicPrimitives.hasOwnProperty("UserTemplates")) {
        	for (var templateName in BasicPrimitives.UserTemplates) {
        		if (BasicPrimitives.UserTemplates.hasOwnProperty(templateName)) {
        			options.templates.push(BasicPrimitives.UserTemplates[templateName].getTemplate());
        		}
        	}
        	options.onItemRender = function (e, data) { BasicPrimitives.UserTemplates[data.templateName].onRender(e, data, options); };
        }
        element.orgDiagram(options);

        if (options.parameters.AutoSizeOnWindowSize)
        {
            jQuery(window).resize(function () { self._onWindowResize(); });
        }

        self._updatePostBackData(element);
    },
    dispose: function ()
    {
        //Add custom dispose actions here
        BasicPrimitives.OrgDiagram.OrgDiagramClientControl.callBaseMethod(this, 'dispose');

        if (this.m_timer != null)
        {
            window.clearTimeout(this.m_timer);
            this.m_timer = null;
        }

        var element = jQuery(this.get_element());
        element.orgDiagram("destroy");
    },
    _onWindowResize: function()
    {
        var self = this;
        if (self.m_timer == null)
        {
            self.m_timer = window.setTimeout(function ()
            {
                var element = jQuery(self.get_element());
                element.orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh);

                window.clearTimeout(self.m_timer);
                self.m_timer = null;
            }, 300);
        }
    },
    _readWidgetParameters: function (element, options)
    {
    	var buttons, button, index, len, buttonConfig;

        options.parameters = element.data("parameters");

    	// Widget Options
        options.defaultTemplateName = options.parameters.DefaultTemplateName;
        options.minimalVisibility = options.parameters.MinimalVisibility;
        options.horizontalAlignment = options.parameters.HorizontalAlignmentType;
        options.verticalAlignment = options.parameters.VerticalAlignmentType;
        options.connectorType = options.parameters.ConnectorType;
        options.childrenPlacementType = options.parameters.ChildrenPlacementType;
        options.leavesPlacementType = options.parameters.LeavesPlacementType;
        options.maximumColumnsInMatrix = options.parameters.MaximumColumnsInMatrix;
        options.selectionPathMode = options.parameters.SelectionPathMode;
        options.pageFitMode = options.parameters.PageFitMode;
        options.orientationType = options.parameters.OrientationType;
        options.hasSelectorCheckbox = options.parameters.ShowCheckBoxes;
        options.hasButtons = options.parameters.ShowButtons;

        options.normalLevelShift = options.parameters.NormalLevelShift;
        options.dotLevelShift = options.parameters.DotLevelShift;
        options.lineLevelShift = options.parameters.LineLevelShift;
        options.normalItemsInterval = options.parameters.NormalItemsInterval;
        options.dotItemsInterval = options.parameters.DotItemsInterval;
        options.lineItemsInterval = options.parameters.LineItemsInterval;

        options.buttons = [];
        if (options.parameters.Buttons != null) {
        	buttons = options.parameters.Buttons;
        	for (index = 0, len = buttons.length; index < len; index++) {
        		button = buttons[index];
        		options.buttons.push(new primitives.orgdiagram.ButtonConfig(button.Name, this.m_icons[button.Icon], button.Name));
        	}
        }
    },
    _onButtonClick: function (e, data) {
    	var self = this;
    	var element = jQuery(this.get_element());
    	var data = {
    		clickedButtonName: data.name,
			clickedButtonItemIndex: data.context.id
    	};
    	this._updatePostBackData(element, data);

   		__doPostBack(this.get_id(), "TemplateButtonClick");
    },
    _onSelectionChanged: function (e, data)
    {
        var self = this;
        var element = jQuery(this.get_element());

        this._updatePostBackData(element);

        var parameters = element.orgDiagram("option", "parameters");
        if (parameters.AutoPostBack)
        {
            __doPostBack(this.get_id(), "ItemCheckChanged");
        }
    },
    _onCursorChanging: function (e, data)
    {
        var self = this;
        var element = jQuery(this.get_element());

        this._updatePostBackData(element);

        var parameters = element.orgDiagram("option", "parameters");
        if (parameters.AutoPostBack)
        {
            data.cancel = true;

            __doPostBack(this.get_id(), "SelectedItemChanged");
        }
    },
    _updatePostBackData: function (element, data)
    {
        var options = element.orgDiagram("option");

        var postBackData = {
            selectedItem: null,
            checkedItems : []
        };

        jQuery.extend(postBackData, data);

        if (options.cursorItem != null)
        {
            postBackData.selectedItem = options.cursorItem;
        }
        for (var index = 0; index < options.selectedItems.length; index++)
        {
            var item = options.selectedItems[index];

            postBackData.checkedItems.push(item);
        }

        var id = this.get_id();
        jQuery("#" + id + "_parameters").attr("value", JSON.stringify(postBackData));
    },
    _recursiveReadTreeItems: function (parent, items, options)
    {
    	var self = this,
    		index, len;
    	for(index = 0, len = items.length; index < len; index++) {
    		var item = items[index];

    		var newItem = new primitives.orgdiagram.ItemConfig();
    		newItem.item = item;
    		newItem.id = self.counter++;
    		newItem.parent = (parent != null) ? parent.id : null;
            newItem.title = item.Title;
            newItem.itemTitleColor = self._getColorString(item.TitleColor);
            newItem.groupTitle = item.GroupTitle;
            newItem.groupTitleColor = self._getColorString(item.GroupTitleColor);
            newItem.description = item.Description;
            newItem.image = item.ImageUrl;
            newItem.templateName = item.TemplateName;
            newItem.itemType = item.ItemType;
            newItem.adviserPlacementType = item.AdviserPlacementType;
            newItem.childrenPlacementType = item.ChildrenPlacementType;
            newItem.hasSelectorCheckbox = item.ShowCheckBox;
            newItem.hasButtons = item.ShowButtons;
            newItem.isVisible = item.IsVisible;
            if (item.Selected)
            {
                options.cursorItem = newItem.id;
            }
            if (item.Checked)
            {
                options.selectedItems.push(newItem.id);
            }
            options.items.push(newItem);

            self._recursiveReadTreeItems(newItem, item.Items, options);
        };
    },
    _getColorString: function (xmlColor)
    {
        var rgb = ((xmlColor.R << 16) | (xmlColor.G << 8) | xmlColor.B).toString(16);
        return '#' + "000000".substr(0, 6 - rgb.length) + rgb;
    }
}
BasicPrimitives.OrgDiagram.OrgDiagramClientControl.registerClass('BasicPrimitives.OrgDiagram.OrgDiagramClientControl', Sys.UI.Control);