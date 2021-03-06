﻿$(function() {

	var $a = Tracker;
	var $u = $a.Urls;
	var $r = $a.Roles;
	var $l = $a.Localization;

	$a.Application = (function($apl) {

		function dateEditor(container, options) {
			$('<input name="' + options.field + '" data-text-field="' + options.field
				+ '" data-value-field="' + options.field + '" data-bind="value:'
				+ options.field + '" />')
				.appendTo(container)
				.kendoDatePicker();
		}

		function stateDropDownEditor(container, options) {
			var applicationId = options.model.Id;
			$('<input required data-text-field="StateName" data-value-field="StateId" data-bind="value:' + options.field + '"/>')
				.appendTo(container)
				.kendoDropDownList({
					autoBind: false,
					select: function(e) {
						if ($a.Confirm($l.Pages_StateSetConfirm)) {
							var dataItem = this.dataItem(e.item.index());
							var url = $u.ApplicationUpdate_SetState;
							$.post(url, { stateId: dataItem.StateId, id: applicationId }).done($apl.UpdateGrid).fail($a.ShowError);
						} else {
							$apl.GetGrid().closeCell();
						}
					},
					dataTextField: "StateName",
					dataValueField: "StateId",
					dataSource: {
						type: "json",
						transport: {
							read: {
								cache: false,
								dataType: "json",
								url: $u.ApplicationUpdate_States + "/" + applicationId,
								type: "POST"
							}
						}
					}
				});
		}

		var n2Format = "{0:n2}";
		var weightField = {
			field: "Weight",
			title: $l.Entities_Weight,
			groupable: false,
			width: "46px",
			groupFooterTemplate: "#= kendo.toString(Weight.sum, 'n2') #",
			format: n2Format
		};
		var documentWeightField = {
			field: "DocumentWeight",
			title: $l.Entities_DocumentWeight,
			groupable: false,
			width: "46px",
			groupFooterTemplate: "#= kendo.toString(DocumentWeight.sum, 'n2') #",
			format: n2Format
		};
		var n0Format = "{0:n0}";
		var countField = {
			field: "Count",
			title: $l.Entities_Count,
			groupable: false,
			width: "35px",
			groupFooterTemplate: "#= Count.sum #",
			format: n0Format
		};
		var volumeField = {
			field: "Volume",
			title: $l.Entities_Volume,
			groupable: false,
			width: "46px",
			groupFooterTemplate: "#= kendo.toString(Volume.sum, 'n2') #",
			format: n2Format
		};
		var valueField = {
			field: "ValueString",
			title: $l.Entities_Value,
			groupable: false,
			groupFooterTemplate: "#= kendo.toString(Value.sum, 'n2') #",
			width: "70px"
		};
		
		function groupHeaderTemplateAwb(data) {
			if (!!data.value) {
				var json = $.parseJSON(data.value);
				return $l.Entities_AWB + ': ' + json.text;
			}

			return $l.Entities_AWB + ': ' + $l.Pages_NoAirWaybill;
		};
		
		function awbHeaderTemplate(data) {
			if (!!data.value) {
				var json = $.parseJSON(data.value);
				return $l.Entities_AWB + ': ' + "<a href='" + $u.AdminAwb_Edit + "/" + json.id + "'>"
					+ json.text + "</a>";
			}

			return $l.Entities_AWB + ': ' + $l.Pages_NoAirWaybill;
		};
		
		var adminColumns = [
			{ field: "CreationTimestampLocalString", title: $l.Entities_CreationTimestamp, groupable: false, width: "90px" },
			{ field: "StateChangeTimestampLocalString", title: $l.Entities_StateChangeTimestamp, groupable: false, width: "90px" },
			{ field: "State", title: $l.Entities_StateName, groupable: true, width: "150px", editor: stateDropDownEditor, template: "#= State.StateName #" },
			{ field: "DateOfCargoReceiptLocalString", title: $l.Entities_DateOfCargoReceipt, groupable: false, editor: dateEditor, width: "90px", template: "#= DateOfCargoReceiptLocalString == null ? '' : kendo.toString(DateOfCargoReceiptLocalString, 'd') #" },
			{ field: "DateInStockLocalString", title: $l.Entities_DateInStock, groupable: false, width: "90px" },
			{ field: "ClientNic", title: $l.Entities_Nic, groupable: true, width: "100px" },
			{
				field: "DisplayNumber",
				title: $l.Entities_DisplayNumber,
				template: "<a href='" + $u.Application_Edit + "/#=Id#'>#= DisplayNumber #</a>",
				groupable: false,
				width: "60px"
			},
			{ field: "CountryName", title: $l.Entities_Country, groupable: true, width: "70px" },
			{ field: "FactoryName", title: $l.Entities_FactoryName, groupable: true, width: "100px" },
			{ field: "MarkName", title: $l.Entities_Mark, groupable: true, width: "100px" },
			countField,
			weightField,
			volumeField,
			{ field: "Invoice", title: $l.Entities_Invoice, groupable: false, width: "150px" },
			{ field: "MRN", title: $l.Entities_MRN, groupable: false, width: "150px" },
			{ field: "CountInInvoce", title: $l.Entities_CountInInvoce, groupable: false, width: "46px" },
			documentWeightField,
			valueField,
			{ field: "SenderName", title: $l.Entities_Sender, groupable: true, width: "100px" },
			{ field: "ForwarderName", title: $l.Entities_Forwarder, groupable: true, width: "100px" },
			{ field: "TransitCity", title: $l.Entities_City, groupable: true, width: "100px" },
			{ field: "CarrierName", title: $l.Entities_Carrier, groupable: true, width: "100px" },
			{ field: "TransitMethodOfTransitString", title: $l.Entities_MethodOfTransit, groupable: true, width: "75px" },
			{ field: "MethodOfDeliveryLocalString", title: $l.Entities_MethodOfDelivery, groupable: true, width: "75px" },
			{ field: "TransitReference", title: $l.Entities_TransitReference, groupable: false, width: "150px" },
			{
				field: "AirWaybill",
				title: $l.Entities_AirWaybill,
				groupable: true,
				width: "150px",
				groupHeaderTemplate: awbHeaderTemplate
			}];

		var clientColumns = [
			{ field: "CreationTimestampLocalString", title: $l.Entities_CreationTimestamp, groupable: false, width: "70px" },
			{ field: "StateChangeTimestampLocalString", title: $l.Entities_StateChangeTimestamp, groupable: false, width: "90px" },
			{ field: "State", title: $l.Entities_StateName, groupable: false, template: "#= State.StateName #" },
			{ field: "DateOfCargoReceiptLocalString", title: $l.Entities_DateOfCargoReceipt, groupable: false, width: "70px" },
			{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, width: "70px", groupable: false },
			{ field: "FactoryName", title: $l.Entities_FactoryName, groupable: false },
			{ field: "MarkName", title: $l.Entities_Mark, groupable: false },
			{
				field: "Count",
				title: $l.Entities_Count,
				groupable: false,
				width: "35px",
				format: n0Format
			},
			{
				field: "Weight",
				title: $l.Entities_Weight,
				groupable: false,
				width: "46px",
				format: n2Format
			},
			{ field: "Invoice", title: $l.Entities_Invoice, groupable: false },
			{
				field: "ValueString",
				title: $l.Entities_Value,
				groupable: false,
				width: "70px"
			},
			{ field: "TransitMethodOfTransitString", title: $l.Entities_MethodOfTransit, groupable: false },
			{ field: "CarrierName", title: $l.Entities_Carrier, groupable: false },
			{ field: "TransitReference", title: $l.Entities_TransitReference, groupable: false, width: "150px" },
			{ field: "AirWaybill", title: $l.Entities_AirWaybill, groupable: false, width: "150px", groupHeaderTemplate: groupHeaderTemplateAwb }];

		var carrierColumns = [
			{ field: "State", title: $l.Entities_StateName, groupable: false, editor: stateDropDownEditor, template: "#= State.StateName #", width: "150px" },
            { field: "StateChangeTimestampLocalString", title: $l.Entities_StateChangeTimestamp, groupable: false, width: "90px" },
			{ field: "ClientLegalEntity", title: $l.Entities_LegalEntity, groupable: false, width: "150px" },
			{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, width: "70px", groupable: false },
			countField,
			weightField,
			volumeField,
			{ field: "TransitMethodOfTransitString", title: $l.Entities_MethodOfTransit, groupable: false, width: "75px" },
			{ field: "TransitDeliveryTypeString", title: $l.Entities_DeliveryType, groupable: false, width: "75px" },
			{ field: "TransitCity", title: $l.Entities_City, groupable: false, width: "100px" },
			{ field: "TransitRecipientName", title: $l.Entities_RecipientName, groupable: false, width: "100px" },
			{ field: "TransitAddress", title: $l.Entities_Address, groupable: false, width: "100px" },
			{ field: "TransitPhone", title: $l.Entities_Phone, groupable: false, width: "100px" },
			{ field: "TransitWarehouseWorkingTime", title: $l.Entities_WarehouseWorkingTime, groupable: false, width: "90px" },
			{ field: "TransitReference", title: $l.Entities_TransitReference, groupable: false, width: "150px" },
			{ field: "AirWaybill", title: $l.Entities_AirWaybill, groupable: false, width: "150px", groupHeaderTemplate: groupHeaderTemplateAwb }];

		var senderColumns = [
			{ field: "CreationTimestampLocalString", title: $l.Entities_CreationTimestamp, groupable: false, width: "70px" },
			{ field: "StateChangeTimestampLocalString", title: $l.Entities_StateChangeTimestamp, groupable: false },
			{ field: "ClientNic", title: $l.Entities_Nic, groupable: false },
			{ field: "TransitCity", title: $l.Entities_City, groupable: false, width: "70px" },
			{ field: "MethodOfDeliveryLocalString", title: $l.Entities_MethodOfDelivery, groupable: true, width: "75px" },
			{ field: "State", title: $l.Entities_StateName, groupable: false, editor: stateDropDownEditor, template: "#= State.StateName #" },
			{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, width: "70px", groupable: false },
			{ field: "CountryName", title: $l.Entities_Country, groupable: false, width: "70px" },
			{ field: "FactoryName", title: $l.Entities_FactoryName, groupable: false },
			{ field: "MarkName", title: $l.Entities_Mark, groupable: false },
			countField,
			weightField,
			volumeField,
			{ field: "Invoice", title: $l.Entities_Invoice, groupable: false },
			{ field: "MRN", title: $l.Entities_MRN, groupable: false, width: "150px" },
			{ field: "CountInInvoce", title: $l.Entities_CountInInvoce, groupable: false, width: "46px" },
			documentWeightField,
			valueField,
			{ field: "AirWaybill", title: $l.Entities_AirWaybill, groupable: false, width: "150px", groupHeaderTemplate: groupHeaderTemplateAwb }];

		$apl.GetColumns = function() {
			if ($r.IsClient) {
				return clientColumns;
			} else if ($r.IsSender) {
				return senderColumns;
			} else if ($r.IsAdmin || $r.IsManager) {
				return adminColumns;
			} else if ($r.IsCarrier) {
				return carrierColumns;
			}

			return null;
		};

		return $apl;
	})($a.Application || {});
});