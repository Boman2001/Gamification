using System.Collections.Generic;

namespace Dtos
{
    public class MusicListDto
    {
        public List<SongDto> SongDtos;
    }

    public class SongDto
    {
        public string id;
        public string title;
    }
}