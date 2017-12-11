using Microsoft.AspNetCore.Mvc;
using recru_it.Data;
using recru_it.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recru_it.Extensions
{
    public class CompareTags
    {
        readonly ApplicationDbContext context;

        public CompareTags(
            ApplicationDbContext context)
        {
            this.context = context;
   
        }

        //Algorithm: Find all jobposts by 1 or more tags
        public List<JobPost> GetJobPostsByTags(List<Tag> tags)
        {
            List<JobPost> jobPosts = new List<JobPost>();

            if(tags.Count() == 1)
            {
                return GetJobPostsByTag(tags);
            }
            else
            {
                //multiple tags from jobposts with multiple other tags from whereever
                foreach(var jp in context.JobPosts.ToList())
                {
                    if(jp.Tags != null)
                    {
                        foreach (var jpt in jp.Tags.ToList())
                            {
                                if (tags.Contains(jpt))
                                {
                                    //add it to the list
                                    jobPosts.Add(jp);
                                }
                            }
                    }

                }
            }
            return jobPosts;
        }

        public List<JobPost> GetJobPostsByTag(List<Tag> tags)
        {
            List<JobPost> JobPostsWithTags = new List<JobPost>();

            if (tags.Count() == 1)
            {
                Tag compareTag = tags.Single();
                JobPostsWithTags = context.JobPosts.Where(jp => jp.Tags.Contains(compareTag)).ToList();
            }
            else
            {
                return GetJobPostsByTags(tags);
            }
            return JobPostsWithTags;
        }

        public List<JobApplication> GetJobApplicationsListByTags(List<Tag> tags)
        {
            List<JobApplication> jobApplications = new List<JobApplication>();

            if (tags.Count() == 1)
            {
                return GetJobApplicationsListByTag(tags);
            }
            else
            {
                //multiple tags from jobposts with multiple other tags from whereever
                foreach (var ja in context.JobApplications.ToList())
                {
                    if (ja.Tags != null)
                    {
                        foreach (var jat in ja.Tags.ToList())
                        {
                            if (tags.Contains(jat))
                            {
                                //add it to the list
                                jobApplications.Add(ja);
                            }
                        }
                    }
                }
            }
            return jobApplications;

        }




        public List<JobApplication> GetJobApplicationsListByTag(List<Tag> tags)
        {

            List<JobApplication> jobApplicationsWithTags = new List<JobApplication>();

            if (tags.Count() == 1)
            {
                Tag compareTag = tags.Single();
                jobApplicationsWithTags = context.JobApplications.Where(jp => jp.Tags.Contains(compareTag)).ToList();
            }
            else
            {
                return GetJobApplicationsListByTags(tags);
            }
            return jobApplicationsWithTags;
        }

    }
}
