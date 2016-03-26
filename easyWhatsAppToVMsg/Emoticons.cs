using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyWhatsAppToVMsg
{
    public static class Emoticons
    {
        /* maps Apple/Softbank-Emoticons (used by WP-WhatsApp to Unicode
         * Warnings:
         * - some unicode-emoticons mapped the same softbank-emoticon, so disabled other ones
         * - softbank-emoticons composed of two chars not supported
         * - unicode-emoticons composed of more than one character not supported
         * 
         * automatically generated from http://unicodey.com/emoji-data/table.htm
         */
        private static readonly Dictionary<char, int> CharMap = new Dictionary<char, int>
        {
            //{ (char)0xe04a 0xe049, 0x26c5 },
            //{ (char)0xe103 0xe328, 0x1f48c },
            //{ (char)0xe210, 0x0023 0x20e3 },
            //{ (char)0xe21c, 0x0031 0x20e3 },
            //{ (char)0xe21d, 0x0032 0x20e3 },
            //{ (char)0xe21e, 0x0033 0x20e3 },
            //{ (char)0xe21f, 0x0034 0x20e3 },
            //{ (char)0xe220, 0x0035 0x20e3 },
            //{ (char)0xe221, 0x0036 0x20e3 },
            //{ (char)0xe222, 0x0037 0x20e3 },
            //{ (char)0xe223, 0x0038 0x20e3 },
            //{ (char)0xe224, 0x0039 0x20e3 },
            //{ (char)0xe225, 0x0030 0x20e3 },
            //{ (char)0xe415 0xe331, 0x1f605 },
            //{ (char)0xe50b, 0x1f1ef 0x1f1f5 },
            //{ (char)0xe50c, 0x1f1fa 0x1f1f8 },
            //{ (char)0xe50d, 0x1f1eb 0x1f1f7 },
            //{ (char)0xe50e, 0x1f1e9 0x1f1ea },
            //{ (char)0xe50f, 0x1f1ee 0x1f1f9 },
            //{ (char)0xe510, 0x1f1ec 0x1f1e7 },
            //{ (char)0xe511, 0x1f1ea 0x1f1f8 },
            //{ (char)0xe512, 0x1f1f7 0x1f1fa },
            //{ (char)0xe513, 0x1f1e8 0x1f1f3 },
            //{ (char)0xe514, 0x1f1f0 0x1f1f7 },
            { (char)0xe001, 0x1f466 },
            { (char)0xe002, 0x1f467 },
            { (char)0xe003, 0x1f48b },
            { (char)0xe004, 0x1f468 },
            { (char)0xe005, 0x1f469 },
            { (char)0xe006, 0x1f455 },
            /*{ (char)0xe006, 0x1f45a },*/
            { (char)0xe007, 0x1f45e },
            /*{ (char)0xe007, 0x1f45f },*/
            { (char)0xe008, 0x1f4f7 },
            { (char)0xe009, 0x1f4de },
            /*{ (char)0xe009, 0x260e },*/
            { (char)0xe00a, 0x1f4f1 },
            { (char)0xe00b, 0x1f4e0 },
            { (char)0xe00c, 0x1f4bb },
            { (char)0xe00d, 0x1f44a },
            { (char)0xe00e, 0x1f44d },
            { (char)0xe00f, 0x261d },
            { (char)0xe010, 0x270a },
            { (char)0xe011, 0x270c },
            { (char)0xe012, 0x1f64b },
            /*{ (char)0xe012, 0x270b },*/
            { (char)0xe013, 0x1f3bf },
            { (char)0xe014, 0x26f3 },
            { (char)0xe015, 0x1f3be },
            { (char)0xe016, 0x26be },
            { (char)0xe017, 0x1f3c4 },
            { (char)0xe018, 0x26bd },
            { (char)0xe019, 0x1f3a3 },
            /*{ (char)0xe019, 0x1f41f },
            { (char)0xe019, 0x1f421 },*/
            { (char)0xe01a, 0x1f434 },
            { (char)0xe01b, 0x1f697 },
            { (char)0xe01c, 0x26f5 },
            { (char)0xe01d, 0x2708 },
            { (char)0xe01e, 0x1f683 },
            { (char)0xe01f, 0x1f685 },
            { (char)0xe020, 0x2753 },
            { (char)0xe021, 0x2757 },
            { (char)0xe022, 0x2764 },
            { (char)0xe023, 0x1f494 },
            { (char)0xe024, 0x1f550 },
            { (char)0xe025, 0x1f551 },
            { (char)0xe026, 0x1f552 },
            { (char)0xe027, 0x1f553 },
            { (char)0xe028, 0x1f554 },
            { (char)0xe029, 0x1f555 },
            { (char)0xe02a, 0x1f556 },
            { (char)0xe02b, 0x1f557 },
            { (char)0xe02c, 0x1f558 },
            { (char)0xe02d, 0x1f559 },
            /*{ (char)0xe02d, 0x23f0 },*/
            { (char)0xe02e, 0x1f55a },
            { (char)0xe02f, 0x1f55b },
            { (char)0xe030, 0x1f338 },
            { (char)0xe031, 0x1f531 },
            { (char)0xe032, 0x1f339 },
            { (char)0xe033, 0x1f384 },
            { (char)0xe034, 0x1f48d },
            { (char)0xe035, 0x1f48e },
            { (char)0xe036, 0x1f3e0 },
            /*{ (char)0xe036, 0x1f3e1 },*/
            { (char)0xe037, 0x26ea },
            { (char)0xe038, 0x1f3e2 },
            { (char)0xe039, 0x1f689 },
            { (char)0xe03a, 0x26fd },
            { (char)0xe03b, 0x1f5fb },
            { (char)0xe03c, 0x1f3a4 },
            { (char)0xe03d, 0x1f3a5 },
            /*{ (char)0xe03d, 0x1f4f9 },*/
            { (char)0xe03e, 0x1f3b5 },
            { (char)0xe03f, 0x1f511 },
            { (char)0xe040, 0x1f3b7 },
            { (char)0xe041, 0x1f3b8 },
            { (char)0xe042, 0x1f3ba },
            { (char)0xe043, 0x1f374 },
            { (char)0xe044, 0x1f377 },
            /*{ (char)0xe044, 0x1f378 },
            /*{ (char)0xe044, 0x1f379 },*/
            { (char)0xe045, 0x2615 },
            { (char)0xe046, 0x1f370 },
            { (char)0xe047, 0x1f37a },
            { (char)0xe048, 0x26c4 },
            { (char)0xe049, 0x2601 },
            { (char)0xe04a, 0x2600 },
            { (char)0xe04b, 0x2614 },
            { (char)0xe04c, 0x1f313 },
            /*{ (char)0xe04c, 0x1f314 },
            { (char)0xe04c, 0x1f319 },
            { (char)0xe04c, 0x1f31b },*/
            { (char)0xe04d, 0x1f304 },
            { (char)0xe04e, 0x1f47c },
            { (char)0xe04f, 0x1f431 },
            { (char)0xe050, 0x1f42f },
            { (char)0xe051, 0x1f43b },
            { (char)0xe052, 0x1f429 },
            /*{ (char)0xe052, 0x1f436 },*/
            { (char)0xe053, 0x1f42d },
            { (char)0xe054, 0x1f433 },
            { (char)0xe055, 0x1f427 },
            { (char)0xe056, 0x1f60a },
            /*{ (char)0xe056, 0x1f60b },*/
            { (char)0xe057, 0x1f603 },
            /*{ (char)0xe057, 0x1f63a },*/
            { (char)0xe058, 0x1f61e },
            { (char)0xe059, 0x1f620 },
            { (char)0xe05a, 0x1f4a9 },
            { (char)0xe101, 0x1f4ea },
            /*{ (char)0xe101, 0x1f4eb },*/
            { (char)0xe102, 0x1f4ee },
            { (char)0xe103, 0x1f4e7 },
            /*{ (char)0xe103, 0x1f4e8 },
            { (char)0xe103, 0x1f4e9 },
            { (char)0xe103, 0x2709 },*/
            { (char)0xe104, 0x1f4f2 },
            { (char)0xe105, 0x1f61c },
            { (char)0xe106, 0x1f60d },
            /*{ (char)0xe106, 0x1f63b },*/
            { (char)0xe107, 0x1f631 },
            { (char)0xe108, 0x1f613 },
            { (char)0xe109, 0x1f435 },
            { (char)0xe10a, 0x1f419 },
            { (char)0xe10b, 0x1f437 },
            /*{ (char)0xe10b, 0x1f43d },*/
            { (char)0xe10c, 0x1f47d },
            { (char)0xe10d, 0x1f680 },
            { (char)0xe10e, 0x1f451 },
            { (char)0xe10f, 0x1f4a1 },
            { (char)0xe110, 0x1f331 },
            /*{ (char)0xe110, 0x1f33f },
            { (char)0xe110, 0x1f340 },*/
            { (char)0xe111, 0x1f48f },
            { (char)0xe112, 0x1f381 },
            /*{ (char)0xe112, 0x1f4e6 },*/
            { (char)0xe113, 0x1f52b },
            { (char)0xe114, 0x1f50d },
            /*{ (char)0xe114, 0x1f50e },*/
            { (char)0xe115, 0x1f3c3 },
            { (char)0xe116, 0x1f528 },
            { (char)0xe117, 0x1f386 },
            { (char)0xe118, 0x1f341 },
            { (char)0xe119, 0x1f342 },
            { (char)0xe11a, 0x1f47f },
            { (char)0xe11b, 0x1f47b },
            { (char)0xe11c, 0x1f480 },
            { (char)0xe11d, 0x1f525 },
            { (char)0xe11e, 0x1f4bc },
            { (char)0xe11f, 0x1f4ba },
            { (char)0xe120, 0x1f354 },
            { (char)0xe121, 0x26f2 },
            { (char)0xe122, 0x26fa },
            { (char)0xe123, 0x2668 },
            { (char)0xe124, 0x1f3a1 },
            { (char)0xe125, 0x1f3ab },
            { (char)0xe126, 0x1f4bf },
            { (char)0xe127, 0x1f4c0 },
            { (char)0xe128, 0x1f4fb },
            { (char)0xe129, 0x1f4fc },
            { (char)0xe12a, 0x1f4fa },
            { (char)0xe12b, 0x1f47e },
            { (char)0xe12c, 0x303d },
            { (char)0xe12d, 0x1f004 },
            { (char)0xe12e, 0x1f19a },
            { (char)0xe12f, 0x1f4b0 },
            /*{ (char)0xe12f, 0x1f4b2 },
            { (char)0xe12f, 0x1f4b5 },*/
            { (char)0xe130, 0x1f3af },
            { (char)0xe131, 0x1f3c6 },
            { (char)0xe132, 0x1f3c1 },
            { (char)0xe133, 0x1f3b0 },
            { (char)0xe134, 0x1f40e },
            { (char)0xe135, 0x1f6a4 },
            { (char)0xe136, 0x1f6b2 },
            { (char)0xe137, 0x1f6a7 },
            /*{ (char)0xe137, 0x26d4 },*/
            { (char)0xe138, 0x1f6b9 },
            { (char)0xe139, 0x1f6ba },
            { (char)0xe13a, 0x1f6bc },
            { (char)0xe13b, 0x1f489 },
            { (char)0xe13c, 0x1f4a4 },
            { (char)0xe13d, 0x26a1 },
            { (char)0xe13e, 0x1f460 },
            { (char)0xe13f, 0x1f6c0 },
            { (char)0xe140, 0x1f6bd },
            { (char)0xe141, 0x1f50a },
            { (char)0xe142, 0x1f4e2 },
            { (char)0xe143, 0x1f38c },
            { (char)0xe144, 0x1f50f },
            /*{ (char)0xe144, 0x1f510 },
            { (char)0xe144, 0x1f512 },*/
            { (char)0xe145, 0x1f513 },
            { (char)0xe146, 0x1f306 },
            { (char)0xe147, 0x1f373 },
            { (char)0xe148, 0x1f4c7 },
            /*{ (char)0xe148, 0x1f4d2 },
            { (char)0xe148, 0x1f4d3 },
            { (char)0xe148, 0x1f4d4 },
            { (char)0xe148, 0x1f4d5 },
            { (char)0xe148, 0x1f4d6 },
            { (char)0xe148, 0x1f4d7 },
            { (char)0xe148, 0x1f4d8 },
            { (char)0xe148, 0x1f4d9 },
            { (char)0xe148, 0x1f4da },*/
            { (char)0xe149, 0x1f4b1 },
            { (char)0xe14a, 0x1f4b9 },
            /*{ (char)0xe14a, 0x1f4c8 },
            { (char)0xe14a, 0x1f4ca },*/
            { (char)0xe14b, 0x1f4e1 },
            { (char)0xe14c, 0x1f4aa },
            { (char)0xe14d, 0x1f3e6 },
            { (char)0xe14e, 0x1f6a5 },
            { (char)0xe14f, 0x1f17f },
            { (char)0xe150, 0x1f68f },
            { (char)0xe151, 0x1f6bb },
            { (char)0xe152, 0x1f46e },
            { (char)0xe153, 0x1f3e3 },
            { (char)0xe154, 0x1f3e7 },
            { (char)0xe155, 0x1f3e5 },
            { (char)0xe156, 0x1f3ea },
            { (char)0xe157, 0x1f3eb },
            { (char)0xe158, 0x1f3e8 },
            { (char)0xe159, 0x1f68c },
            { (char)0xe15a, 0x1f695 },
            { (char)0xe201, 0x1f6b6 },
            { (char)0xe202, 0x1f6a2 },
            /*{ (char)0xe202, 0x2693 },*/
            { (char)0xe203, 0x1f201 },
            { (char)0xe204, 0x1f49f },
            { (char)0xe205, 0x2734 },
            { (char)0xe206, 0x2733 },
            { (char)0xe207, 0x1f51e },
            { (char)0xe208, 0x1f6ad },
            { (char)0xe209, 0x1f530 },
            { (char)0xe20a, 0x267f },
            { (char)0xe20b, 0x1f4f6 },
            { (char)0xe20c, 0x2665 },
            { (char)0xe20d, 0x2666 },
            { (char)0xe20e, 0x2660 },
            { (char)0xe20f, 0x2663 },
            { (char)0xe211, 0x27bf },
            { (char)0xe212, 0x1f195 },
            { (char)0xe213, 0x1f199 },
            { (char)0xe214, 0x1f192 },
            { (char)0xe215, 0x1f236 },
            { (char)0xe216, 0x1f21a },
            { (char)0xe217, 0x1f237 },
            { (char)0xe218, 0x1f238 },
            { (char)0xe219, 0x1f534 },
            /*{ (char)0xe219, 0x26aa },
            { (char)0xe219, 0x26ab },*/
            { (char)0xe21a, 0x1f532 },
            /*{ (char)0xe21a, 0x1f535 },
            { (char)0xe21a, 0x25aa },
            { (char)0xe21a, 0x25fc },
            { (char)0xe21a, 0x25fe },
            { (char)0xe21a, 0x2b1b },*/
            { (char)0xe21b, 0x1f533 },
            /*{ (char)0xe21b, 0x1f536 },
            { (char)0xe21b, 0x1f537 },
            { (char)0xe21b, 0x1f538 },
            { (char)0xe21b, 0x1f539 },
            { (char)0xe21b, 0x25ab },
            { (char)0xe21b, 0x25fb },
            { (char)0xe21b, 0x25fd },
            { (char)0xe21b, 0x2b1c },*/
            { (char)0xe226, 0x1f250 },
            { (char)0xe227, 0x1f239 },
            { (char)0xe228, 0x1f202 },
            { (char)0xe229, 0x1f194 },
            { (char)0xe22a, 0x1f235 },
            { (char)0xe22b, 0x1f233 },
            { (char)0xe22c, 0x1f22f },
            { (char)0xe22d, 0x1f23a },
            { (char)0xe22e, 0x1f446 },
            { (char)0xe22f, 0x1f447 },
            { (char)0xe230, 0x1f448 },
            { (char)0xe231, 0x1f449 },
            { (char)0xe232, 0x2b06 },
            { (char)0xe233, 0x2b07 },
            { (char)0xe234, 0x27a1 },
            { (char)0xe235, 0x1f519 },
            /*{ (char)0xe235, 0x2b05 },*/
            { (char)0xe236, 0x2197 },
            /*{ (char)0xe236, 0x2934 },*/
            { (char)0xe237, 0x2196 },
            { (char)0xe238, 0x2198 },
            /*{ (char)0xe238, 0x2935 },*/
            { (char)0xe239, 0x2199 },
            { (char)0xe23a, 0x25b6 },
            { (char)0xe23b, 0x25c0 },
            { (char)0xe23c, 0x23e9 },
            { (char)0xe23d, 0x23ea },
            { (char)0xe23e, 0x1f52e },
            /*{ (char)0xe23e, 0x1f52f },*/
            { (char)0xe23f, 0x2648 },
            { (char)0xe240, 0x2649 },
            { (char)0xe241, 0x264a },
            { (char)0xe242, 0x264b },
            { (char)0xe243, 0x264c },
            { (char)0xe244, 0x264d },
            { (char)0xe245, 0x264e },
            { (char)0xe246, 0x264f },
            { (char)0xe247, 0x2650 },
            { (char)0xe248, 0x2651 },
            { (char)0xe249, 0x2652 },
            { (char)0xe24a, 0x2653 },
            { (char)0xe24b, 0x26ce },
            { (char)0xe24c, 0x1f51d },
            { (char)0xe24d, 0x1f197 },
            { (char)0xe24e, 0x00a9 },
            { (char)0xe24f, 0x00ae },
            { (char)0xe250, 0x1f4f3 },
            { (char)0xe251, 0x1f4f4 },
            { (char)0xe252, 0x26a0 },
            { (char)0xe253, 0x1f481 },
            { (char)0xe301, 0x1f4c3 },
            /*{ (char)0xe301, 0x1f4c4 },
            { (char)0xe301, 0x1f4cb },
            { (char)0xe301, 0x1f4d1 },
            { (char)0xe301, 0x1f4dd },
            { (char)0xe301, 0x270f },*/
            { (char)0xe302, 0x1f454 },
            { (char)0xe303, 0x1f33a },
            { (char)0xe304, 0x1f337 },
            { (char)0xe305, 0x1f33b },
            /*{ (char)0xe305, 0x1f33c },*/
            { (char)0xe306, 0x1f490 },
            { (char)0xe307, 0x1f334 },
            { (char)0xe308, 0x1f335 },
            { (char)0xe309, 0x1f6be },
            { (char)0xe30a, 0x1f3a7 },
            { (char)0xe30b, 0x1f376 },
            /*{ (char)0xe30b, 0x1f3ee },*/
            { (char)0xe30c, 0x1f37b },
            { (char)0xe30d, 0x3297 },
            { (char)0xe30e, 0x1f6ac },
            { (char)0xe30f, 0x1f48a },
            { (char)0xe310, 0x1f388 },
            { (char)0xe311, 0x1f4a3 },
            { (char)0xe312, 0x1f389 },
            { (char)0xe313, 0x2702 },
            { (char)0xe314, 0x1f380 },
            { (char)0xe315, 0x3299 },
            { (char)0xe316, 0x1f4bd },
            /*{ (char)0xe316, 0x1f4be },*/
            { (char)0xe317, 0x1f4e3 },
            { (char)0xe318, 0x1f452 },
            { (char)0xe319, 0x1f457 },
            { (char)0xe31a, 0x1f461 },
            { (char)0xe31b, 0x1f462 },
            { (char)0xe31c, 0x1f484 },
            { (char)0xe31d, 0x1f485 },
            { (char)0xe31e, 0x1f486 },
            { (char)0xe31f, 0x1f487 },
            { (char)0xe320, 0x1f488 },
            { (char)0xe321, 0x1f458 },
            { (char)0xe322, 0x1f459 },
            { (char)0xe323, 0x1f45c },
            { (char)0xe324, 0x1f3ac },
            { (char)0xe325, 0x1f514 },
            { (char)0xe326, 0x1f3b6 },
            /*{ (char)0xe326, 0x1f3bc },*/
            { (char)0xe327, 0x1f493 },
            /*{ (char)0xe327, 0x1f495 },
            { (char)0xe327, 0x1f496 },
            { (char)0xe327, 0x1f49e },*/
            { (char)0xe328, 0x1f497 },
            { (char)0xe329, 0x1f498 },
            { (char)0xe32a, 0x1f499 },
            { (char)0xe32b, 0x1f49a },
            { (char)0xe32c, 0x1f49b },
            { (char)0xe32d, 0x1f49c },
            { (char)0xe32e, 0x2728 },
            /*{ (char)0xe32e, 0x2747 },*/
            { (char)0xe32f, 0x2b50 },
            { (char)0xe330, 0x1f4a8 },
            { (char)0xe331, 0x1f4a6 },
            /*{ (char)0xe331, 0x1f4a7 },*/
            { (char)0xe332, 0x2b55 },
            { (char)0xe333, 0x2716 },
            /*{ (char)0xe333, 0x274c },
            { (char)0xe333, 0x274e },*/
            { (char)0xe334, 0x1f4a2 },
            { (char)0xe335, 0x1f31f },
            { (char)0xe336, 0x2754 },
            { (char)0xe337, 0x2755 },
            { (char)0xe338, 0x1f375 },
            { (char)0xe339, 0x1f35e },
            { (char)0xe33a, 0x1f366 },
            { (char)0xe33b, 0x1f35f },
            { (char)0xe33c, 0x1f361 },
            { (char)0xe33d, 0x1f358 },
            { (char)0xe33e, 0x1f35a },
            { (char)0xe33f, 0x1f35d },
            { (char)0xe340, 0x1f35c },
            { (char)0xe341, 0x1f35b },
            { (char)0xe342, 0x1f359 },
            { (char)0xe343, 0x1f362 },
            { (char)0xe344, 0x1f363 },
            { (char)0xe345, 0x1f34e },
            /*{ (char)0xe345, 0x1f34f },*/
            { (char)0xe346, 0x1f34a },
            { (char)0xe347, 0x1f353 },
            { (char)0xe348, 0x1f349 },
            { (char)0xe349, 0x1f345 },
            { (char)0xe34a, 0x1f346 },
            { (char)0xe34b, 0x1f382 },
            { (char)0xe34c, 0x1f371 },
            { (char)0xe34d, 0x1f372 },
            { (char)0xe401, 0x1f625 },
            { (char)0xe402, 0x1f60f },
            { (char)0xe403, 0x1f614 },
            /*{ (char)0xe403, 0x1f629 },
            { (char)0xe403, 0x1f640 },
            { (char)0xe403, 0x1f64d },*/
            { (char)0xe404, 0x1f601 },
            /*{ (char)0xe404, 0x1f624 },
            { (char)0xe404, 0x1f638 },
            { (char)0xe404, 0x1f63c },*/
            { (char)0xe405, 0x1f609 },
            { (char)0xe406, 0x1f623 },
            /*{ (char)0xe406, 0x1f62b },
            { (char)0xe406, 0x1f635 },*/
            { (char)0xe407, 0x1f4ab },
            /*{ (char)0xe407, 0x1f616 },*/
            { (char)0xe408, 0x1f62a },
            { (char)0xe409, 0x1f445 },
            /*{ (char)0xe409, 0x1f61d },*/
            { (char)0xe40a, 0x1f606 },
            /*{ (char)0xe40a, 0x1f60c },*/
            { (char)0xe40b, 0x1f628 },
            { (char)0xe40c, 0x1f637 },
            { (char)0xe40d, 0x1f633 },
            { (char)0xe40e, 0x1f612 },
            { (char)0xe40f, 0x1f630 },
            { (char)0xe410, 0x1f632 },
            { (char)0xe411, 0x1f62d },
            { (char)0xe412, 0x1f602 },
            /*{ (char)0xe412, 0x1f639 },*/
            { (char)0xe413, 0x1f622 },
            /*{ (char)0xe413, 0x1f63f },*/
            { (char)0xe414, 0x263a },
            { (char)0xe415, 0x1f604 },
            { (char)0xe416, 0x1f621 },
            /*{ (char)0xe416, 0x1f63e },
            { (char)0xe416, 0x1f64e },*/
            { (char)0xe417, 0x1f61a },
            { (char)0xe418, 0x1f618 },
            /*{ (char)0xe418, 0x1f63d },*/
            { (char)0xe419, 0x1f440 },
            { (char)0xe41a, 0x1f443 },
            { (char)0xe41b, 0x1f442 },
            { (char)0xe41c, 0x1f444 },
            { (char)0xe41d, 0x1f64f },
            { (char)0xe41e, 0x1f44b },
            { (char)0xe41f, 0x1f44f },
            { (char)0xe420, 0x1f44c },
            { (char)0xe421, 0x1f44e },
            { (char)0xe422, 0x1f450 },
            { (char)0xe423, 0x1f645 },
            { (char)0xe424, 0x1f646 },
            { (char)0xe425, 0x1f491 },
            { (char)0xe426, 0x1f647 },
            { (char)0xe427, 0x1f64c },
            { (char)0xe428, 0x1f46b },
            { (char)0xe429, 0x1f46f },
            { (char)0xe42a, 0x1f3c0 },
            { (char)0xe42b, 0x1f3c8 },
            { (char)0xe42c, 0x1f3b1 },
            { (char)0xe42d, 0x1f3ca },
            { (char)0xe42e, 0x1f699 },
            { (char)0xe42f, 0x1f69a },
            { (char)0xe430, 0x1f692 },
            { (char)0xe431, 0x1f691 },
            { (char)0xe432, 0x1f693 },
            /*{ (char)0xe432, 0x1f6a8 },*/
            { (char)0xe433, 0x1f3a2 },
            { (char)0xe434, 0x1f687 },
            /*{ (char)0xe434, 0x24c2 },*/
            { (char)0xe435, 0x1f684 },
            { (char)0xe436, 0x1f38d },
            { (char)0xe437, 0x1f49d },
            { (char)0xe438, 0x1f38e },
            { (char)0xe439, 0x1f393 },
            { (char)0xe43a, 0x1f392 },
            { (char)0xe43b, 0x1f38f },
            { (char)0xe43c, 0x1f302 },
            { (char)0xe43d, 0x1f492 },
            { (char)0xe43e, 0x1f30a },
            { (char)0xe43f, 0x1f367 },
            { (char)0xe440, 0x1f387 },
            { (char)0xe441, 0x1f41a },
            { (char)0xe442, 0x1f390 },
            { (char)0xe443, 0x1f300 },
            { (char)0xe444, 0x1f33e },
            { (char)0xe445, 0x1f383 },
            { (char)0xe446, 0x1f391 },
            { (char)0xe447, 0x1f343 },
            { (char)0xe448, 0x1f385 },
            { (char)0xe449, 0x1f305 },
            { (char)0xe44a, 0x1f307 },
            { (char)0xe44b, 0x1f303 },
            /*{ (char)0xe44b, 0x1f309 },
            { (char)0xe44b, 0x1f30c },*/
            { (char)0xe44c, 0x1f308 },
            { (char)0xe501, 0x1f3e9 },
            { (char)0xe502, 0x1f3a8 },
            { (char)0xe503, 0x1f3a9 },
            /*{ (char)0xe503, 0x1f3ad },*/
            { (char)0xe504, 0x1f3ec },
            { (char)0xe505, 0x1f3ef },
            { (char)0xe506, 0x1f3f0 },
            { (char)0xe507, 0x1f3a6 },
            { (char)0xe508, 0x1f3ed },
            { (char)0xe509, 0x1f5fc },
            { (char)0xe515, 0x1f471 },
            { (char)0xe516, 0x1f472 },
            { (char)0xe517, 0x1f473 },
            { (char)0xe518, 0x1f474 },
            { (char)0xe519, 0x1f475 },
            { (char)0xe51a, 0x1f476 },
            { (char)0xe51b, 0x1f477 },
            { (char)0xe51c, 0x1f478 },
            { (char)0xe51d, 0x1f5fd },
            { (char)0xe51e, 0x1f482 },
            { (char)0xe51f, 0x1f483 },
            { (char)0xe520, 0x1f42c },
            { (char)0xe521, 0x1f426 },
            { (char)0xe522, 0x1f420 },
            { (char)0xe523, 0x1f423 },
            /*{ (char)0xe523, 0x1f424 },
            { (char)0xe523, 0x1f425 },*/
            { (char)0xe524, 0x1f439 },
            { (char)0xe525, 0x1f41b },
            { (char)0xe526, 0x1f418 },
            { (char)0xe527, 0x1f428 },
            { (char)0xe528, 0x1f412 },
            { (char)0xe529, 0x1f411 },
            { (char)0xe52a, 0x1f43a },
            { (char)0xe52b, 0x1f42e },
            { (char)0xe52c, 0x1f430 },
            { (char)0xe52d, 0x1f40d },
            { (char)0xe52e, 0x1f414 },
            { (char)0xe52f, 0x1f417 },
            { (char)0xe530, 0x1f42b },
            { (char)0xe531, 0x1f438 },
            { (char)0xe532, 0x1f170 },
            { (char)0xe533, 0x1f171 },
            { (char)0xe534, 0x1f18e },
            { (char)0xe535, 0x1f17e },
            { (char)0xe536, 0x1f43e },
            /*{ (char)0xe536, 0x1f463 },*/
            { (char)0xe537, 0x2122 },

        };

        public static byte[] replaceEmojis(char[] c) {
            byte[] r = new byte[c.Length * 4]; //TODO: nothing added?
            int j = 0;

            int i;
            for (i = 0; i < c.Length; i++)
            {
                // First work out all the tricky Unicode parts...
                int codePoint = c[i];
                if (isPrivateUseArea(codePoint) && CharMap.ContainsKey((char)codePoint))
                {
                    codePoint = CharMap[(char)codePoint];
                }
                if (isHighSurrogate(codePoint) && i < c.Length - 1 && isLowSurrogate(c[i + 1]))
                {
                    var high = codePoint;
                    var low = c[i + 1];
                    codePoint = combineSurrogates(high, low);
                    i++;
                }
                foreach (var b in toUtf8(codePoint))
                {
                    r[j++] = (byte)b;
                }
            }

            i = 0;
            Array.Reverse(r);
            while (r[i] == 0)
                i++;
            Array.Reverse(r);
            Array.Resize(ref r, r.Length - i);

            var test = Encoding.UTF8.GetString(r);
            return r;
        }

        /* Source: http://csharpindepth.com/Articles/General/Unicode.aspx
         * 
         * Private use as per
         * https://de.wikipedia.org/wiki/Unicode#PUA_.28.E2.80.9CPrivate_Use_Area.E2.80.9D.2C_privat_nutzbarer_Bereich.29
         */
        private static bool isPrivateUseArea(int codeUnit) {
            return codeUnit >= 0xe000 && codeUnit <= 0xf8ff;
        }

        /* Source: http://csharpindepth.com/Articles/General/Unicode.aspx
         * 
         * Surrogate functions as per
         * http://en.wikipedia.org/wiki/Mapping_of_Unicode_characters#Surrogates
         */
        private static bool isHighSurrogate(int codeUnit) {
            return codeUnit >= 0xd800 && codeUnit <= 0xdbff;
        }
        private static bool isLowSurrogate(int codeUnit) {
            return codeUnit >= 0xdc00 && codeUnit <= 0xdfff;
        }
        private static int combineSurrogates(int high, int low) {
            return 0x10000 + (high - 0xd800) * 0x400 + (low - 0xdc00);
        }

        /* Source: http://csharpindepth.com/Articles/General/Unicode.aspx
         * 
         * Takes the (count) bits starting at bit (firstBit), and returns
         * that many bits as the lowest bits of the result. Javascript treats
         * bitshifting as if all values are signed 32-bit integers, but that's
         * okay in our case: we don't use more than 21 bits.
         */
        private static int extractAndShift(int value, int firstBit, int count)
        {
            // This has the correct bottom-most bits, but also information to remove.
            var shifted = value >> (firstBit - count);
            // This is the same as (shifted), but with the bottom-most bits all cleared.
            // In other words, this is the part to remove.
            var extra = (value >> firstBit) << count;
            return shifted - extra;
        }

        /* Source: http://csharpindepth.com/Articles/General/Unicode.aspx
         * 
         * Converts a full Unicode code point (including non-BMP) to UTF- 8 as per
         * http://en.wikipedia.org/wiki/UTF-8#Description, but only the first four
         * rows as we don't understand values above U+10FFFF anyway.
         */
        private static int[] toUtf8(int codePoint)
        {
            if (codePoint < 0x80)
            {
                return new int[] { codePoint };
            }
            else if (codePoint < 0x800)
            {
                return new int[] {
                        0xc0 | extractAndShift(codePoint, 11, 5),
                        0x80 | extractAndShift(codePoint, 6, 6) };
            }
            else if (codePoint < 0x10000)
            {
                return new int[] {
                        0xe0 | extractAndShift(codePoint, 16, 4),
                        0x80 | extractAndShift(codePoint, 12, 6),
                        0x80 | extractAndShift(codePoint, 6, 6) };
            }
            else
            {
                return new int[] {
                        0xf0 | extractAndShift(codePoint, 21, 3),
                        0x80 | extractAndShift(codePoint, 18, 6),
                        0x80 | extractAndShift(codePoint, 12, 6),
                        0x80 | extractAndShift(codePoint, 6, 6) };
            }
        }

    }
}
