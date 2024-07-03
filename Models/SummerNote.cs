namespace aspnetcoremvc.Models{
    public class SummerNote{
        public string ID {set; get;} = string.Empty;

        public int Height {set; get;}

        public string ToolBar = @"
        [
            ['style', ['style']],
            ['font', ['bold', 'underline', 'clear']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'video']],
            ['view', ['fullscreen', 'codeview', 'help']]
        ]";
    }
}