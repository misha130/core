/*
 * Header slides
 ----------------
 * This code powers the 'header slides', which are at the core of mobile nav
*/
var _this = this;
var headerSlideTriggers = document.querySelectorAll('[data-trigger-header-slide]');
var _loop_1 = function (i) {
    var trigger = headerSlideTriggers.item(i);
    headerSlideTriggers[i].addEventListener('click', function (e) {
        var headerSlide = document.querySelector(trigger.getAttribute('data-trigger-header-slide'));
        headerSlide.classList.toggle('is-active');
        _this.classList.toggle('is-active');
        // Position header slide appropriately relative to
        // trigger.
        var rect = _this.getBoundingClientRect();
        headerSlide.style.top = (rect.top + rect.height) + "px";
        headerSlide.style.right = (document.body.clientWidth - rect.right) + "px";
        // Prevent navigation
        e.preventDefault();
    });
};
for (var i = 0; i < headerSlideTriggers.length; i++) {
    _loop_1(i);
}
/*
 * Date/Time functions
 ----------------
 * Utilities to handle dates
*/
function utcDateTimeToLocalDisplay(date) {
    var dateInstance = new Date(date);
    var dateOptions = { year: 'numeric', month: 'numeric', day: 'numeric' };
    var timeOptions = { hour12: false };
    var locale = navigator.language;
    return dateInstance.toLocaleDateString(locale, dateOptions) + " " + dateInstance.toLocaleTimeString(locale, timeOptions);
}
/**
 * Switches all dates from UTC to local client date
 */
window.addEventListener('load', function () {
    var dateElements = document.querySelectorAll('.live-date');
    for (var i = 0; i < dateElements.length; i++) {
        var dateElement = dateElements.item(i);
        var dateAttribue = dateElement.attributes.getNamedItem('data-date');
        if (dateAttribue) {
            var date = dateAttribue.value;
            dateElement.innerHTML = utcDateTimeToLocalDisplay(date);
        }
    }
});
//# sourceMappingURL=site.js.map