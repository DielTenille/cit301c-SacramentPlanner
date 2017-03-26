using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SacramentPlanner.Models
{
    public partial class SacramentPlannerContext : DbContext
    {
        public SacramentPlannerContext(DbContextOptions<SacramentPlannerContext> options) : base(options)
        {

        }

        public virtual DbSet<Calling> Calling { get; set; }
        public virtual DbSet<Hymn> Hymn { get; set; }
        public virtual DbSet<HymnType> HymnType { get; set; }
        public virtual DbSet<Prayer> Prayer { get; set; }
        public virtual DbSet<PrayerType> PrayerType { get; set; }
        public virtual DbSet<SacramentMeeting> SacramentMeeting { get; set; }
        public virtual DbSet<Speaker> Speaker { get; set; }
        public virtual DbSet<SpeakerType> SpeakerType { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<WardMember> WardMember { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calling>(entity =>
            {
                entity.Property(e => e.CallingId)
                    .HasColumnName("CallingID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bishopric).HasDefaultValueSql("0");

                entity.Property(e => e.CallingName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.OtherLeader).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Hymn>(entity =>
            {
                entity.Property(e => e.HymnId)
                    .HasColumnName("HymnID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkHymnType).HasColumnName("fk_HymnType");

                entity.Property(e => e.HymnTitle)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.FkHymnTypeNavigation)
                    .WithMany(p => p.Hymn)
                    .HasForeignKey(d => d.FkHymnType)
                    .HasConstraintName("fk_Hymn_HymnType");
            });

            modelBuilder.Entity<HymnType>(entity =>
            {
                entity.Property(e => e.HymnTypeId)
                    .HasColumnName("HymnTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.HymnType1)
                    .IsRequired()
                    .HasColumnName("Hymn_Type")
                    .HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<Prayer>(entity =>
            {
                entity.Property(e => e.PrayerId)
                    .HasColumnName("PrayerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkPrayerType).HasColumnName("fk_PrayerType");

                entity.Property(e => e.FkWardMemberId).HasColumnName("fk_WardMemberID");

                entity.Property(e => e.PrayerDate).HasColumnType("date");

                entity.HasOne(d => d.FkPrayerTypeNavigation)
                    .WithMany(p => p.Prayer)
                    .HasForeignKey(d => d.FkPrayerType)
                    .HasConstraintName("fk_prayer_prayerType");

                entity.HasOne(d => d.FkWardMember)
                    .WithMany(p => p.Prayer)
                    .HasForeignKey(d => d.FkWardMemberId)
                    .HasConstraintName("fk_prayer_wardMember");
            });

            modelBuilder.Entity<PrayerType>(entity =>
            {
                entity.Property(e => e.PrayerTypeId)
                    .HasColumnName("PrayerTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TypePrayer)
                    .IsRequired()
                    .HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<SacramentMeeting>(entity =>
            {
                entity.Property(e => e.SacramentMeetingId)
                    .HasColumnName("SacramentMeetingID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BabyBlessing).HasDefaultValueSql("0");

                entity.Property(e => e.Confirmation).HasDefaultValueSql("0");

                entity.Property(e => e.FkClosingPrayer).HasColumnName("fk_ClosingPrayer");

                entity.Property(e => e.FkClosingSong).HasColumnName("fk_ClosingSong");

                entity.Property(e => e.FkConducting).HasColumnName("fk_Conducting");

                entity.Property(e => e.FkFirstSpeaker).HasColumnName("fk_FirstSpeaker");

                entity.Property(e => e.FkIntermediateSong).HasColumnName("fk_IntermediateSong");

                entity.Property(e => e.FkMeetingTopic).HasColumnName("fk_MeetingTopic");

                entity.Property(e => e.FkMusicLeader).HasColumnName("fk_MusicLeader");

                entity.Property(e => e.FkMusicPlayer).HasColumnName("fk_MusicPlayer");

                entity.Property(e => e.FkOpenPrayer).HasColumnName("fk_OpenPrayer");

                entity.Property(e => e.FkOpenSong).HasColumnName("fk_OpenSong");

                entity.Property(e => e.FkSacramentSong).HasColumnName("fk_SacramentSong");

                entity.Property(e => e.FkSecondSpeaker).HasColumnName("fk_SecondSpeaker");

                entity.Property(e => e.FkWardBusiness).HasColumnName("fk_WardBusiness");

                entity.Property(e => e.FkYouthSpeaker).HasColumnName("fk_YouthSpeaker");

                entity.Property(e => e.SacramentDate).HasColumnType("date");

                entity.HasOne(d => d.FkClosingPrayerNavigation)
                    .WithMany(p => p.SacramentMeetingFkClosingPrayerNavigation)
                    .HasForeignKey(d => d.FkClosingPrayer)
                    .HasConstraintName("fk_closing_meeting_prayer");

                entity.HasOne(d => d.FkClosingSongNavigation)
                    .WithMany(p => p.SacramentMeetingFkClosingSongNavigation)
                    .HasForeignKey(d => d.FkClosingSong)
                    .HasConstraintName("fk_closing_meeting_hymn");

                entity.HasOne(d => d.FkConductingNavigation)
                    .WithMany(p => p.SacramentMeetingFkConductingNavigation)
                    .HasForeignKey(d => d.FkConducting)
                    .HasConstraintName("fk_conducting_meeting_wardMember");

                entity.HasOne(d => d.FkIntermediateSongNavigation)
                    .WithMany(p => p.SacramentMeetingFkIntermediateSongNavigation)
                    .HasForeignKey(d => d.FkIntermediateSong)
                    .HasConstraintName("fk_intermediate_meeting_hymn");

                entity.HasOne(d => d.FkMeetingTopicNavigation)
                    .WithMany(p => p.SacramentMeeting)
                    .HasForeignKey(d => d.FkMeetingTopic)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_meeting_topic");

                entity.HasOne(d => d.FkMusicLeaderNavigation)
                    .WithMany(p => p.SacramentMeetingFkMusicLeaderNavigation)
                    .HasForeignKey(d => d.FkMusicLeader)
                    .HasConstraintName("fk_musicLeader_meeting_wardMember");

                entity.HasOne(d => d.FkMusicPlayerNavigation)
                    .WithMany(p => p.SacramentMeetingFkMusicPlayerNavigation)
                    .HasForeignKey(d => d.FkMusicPlayer)
                    .HasConstraintName("fk_musicPlayer_meeting_wardMember");

                entity.HasOne(d => d.FkOpenPrayerNavigation)
                    .WithMany(p => p.SacramentMeetingFkOpenPrayerNavigation)
                    .HasForeignKey(d => d.FkOpenPrayer)
                    .HasConstraintName("fk_open_meeting_prayer");

                entity.HasOne(d => d.FkOpenSongNavigation)
                    .WithMany(p => p.SacramentMeetingFkOpenSongNavigation)
                    .HasForeignKey(d => d.FkOpenSong)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_open_meeting_hymn");

                entity.HasOne(d => d.FkSacramentSongNavigation)
                    .WithMany(p => p.SacramentMeetingFkSacramentSongNavigation)
                    .HasForeignKey(d => d.FkSacramentSong)
                    .HasConstraintName("fk_sacrament_meeting_hymn");
            });

            modelBuilder.Entity<Speaker>(entity =>
            {
                entity.Property(e => e.SpeakerId)
                    .HasColumnName("SpeakerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkSpeakerType).HasColumnName("fk_SpeakerType");

                entity.Property(e => e.FkTopic).HasColumnName("fk_Topic");

                entity.Property(e => e.FkWardMember).HasColumnName("fk_WardMember");

                entity.Property(e => e.SpeakerDate).HasColumnType("date");

                entity.HasOne(d => d.FkSpeakerTypeNavigation)
                    .WithMany(p => p.Speaker)
                    .HasForeignKey(d => d.FkSpeakerType)
                    .HasConstraintName("fk_speaker_type");

                entity.HasOne(d => d.FkTopicNavigation)
                    .WithMany(p => p.Speaker)
                    .HasForeignKey(d => d.FkTopic)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_speaker_topic");

                entity.HasOne(d => d.FkWardMemberNavigation)
                    .WithMany(p => p.Speaker)
                    .HasForeignKey(d => d.FkWardMember)
                    .HasConstraintName("fk_speaker_wardMember");
            });

            modelBuilder.Entity<SpeakerType>(entity =>
            {
                entity.Property(e => e.SpeakerTypeId)
                    .HasColumnName("SpeakerTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SpeakerType1)
                    .IsRequired()
                    .HasColumnName("Speaker_Type")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId)
                    .HasColumnName("TopicID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TopicTitle)
                    .IsRequired()
                    .HasColumnType("varchar(60)");
            });

            modelBuilder.Entity<WardMember>(entity =>
            {
                entity.Property(e => e.WardMemberId)
                    .HasColumnName("WardMemberID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkCallingId).HasColumnName("fk_CallingID");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.FkCalling)
                    .WithMany(p => p.WardMember)
                    .HasForeignKey(d => d.FkCallingId)
                    .HasConstraintName("fk_wardMember_calling");
            });
        }
    }
}