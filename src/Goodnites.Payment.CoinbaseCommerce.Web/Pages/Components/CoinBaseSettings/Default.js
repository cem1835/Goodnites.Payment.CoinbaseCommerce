(function ($) {
    $(function () {
        var l = abp.localization.getResource('AbpSettingManagement');

        $("#CoinBaseSettings").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();
            
            goodnites.payment.coinbaseCommerce.coinBaseSettings.update(form).then(function (result) {
                $(document).trigger("AbpSettingSaved");
            });
        });
    });
})(jQuery);