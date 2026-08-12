using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Models.Dtos.CommonDtos;
using Synapse.Web.Helpers.SecureAccess;
using Core.Models.Extensions;
using Core.Models.Helpers;

namespace Synapse.Web.Models
{
    public class LayoutMenu
    {
        public List<MenuItem> MenuItems { get; set; }

        public List<KeyValuePair<string, List<MenuItem>>> MenuKeyValuPaires { get; set; }

        public List<UserActions> UserActions { get; set; }
        public LayoutMenu(AuthenticateSecurityClient client, int userId, bool IsSuperAdmin, List<string> stringCollection)
        {
            try
            {
                var outSource = new List<KeyValuePair<string, List<MenuItem>>>();
                MenuItems = buildUserMenu(client, userId, IsSuperAdmin, stringCollection, ref outSource);
                MenuKeyValuPaires = outSource;
                UserActions = (from mainMenu in MenuItems
                               from childMenu in mainMenu.ChildMenuItems
                               select new UserActions
                               {
                                   ActionName = childMenu.ActionName,
                                   ControllerName = childMenu.ControllerName,
                                   IsCheckerRequired = childMenu.IsCheckerRequired
                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error in LayoutMenu :: {0}", ex.ToString());
            }
        }

        public List<MenuItem> buildUserMenu(AuthenticateSecurityClient client, int userId, bool IsSuperAdmin,
            List<string> sCollection, ref List<KeyValuePair<string, List<MenuItem>>> outSource)
        {
            var menuItems = new List<MenuItem>();
            try
            {
                var dbMenuItems = client.GetUserMenuItems(userId);
                if (dbMenuItems.Result != null && dbMenuItems.Result.Any())
                {
                    outSource = BuildUserMenuKeyPairs(dbMenuItems.Result, IsSuperAdmin, sCollection);

                    var featureIds = dbMenuItems.Result.Select(x => x.FeatureId).Distinct().ToList();
                    featureIds.ForEach(x =>
                    {
                        var featureItems = dbMenuItems.Result.Where(w => w.FeatureId == x);
                        if (featureItems.Any())
                        {
                            var feature = featureItems.FirstOrDefault();
                            menuItems.Add(new MenuItem
                            {
                                Name = feature.MenuName,
                                ArabicName = feature.ArabicName,
                                MenuIcon = feature.MenuIcon,
                                ChildMenuItems = featureItems.Select(s => new SubMenuItems
                                {
                                    Name = s.SubFeature,
                                    ArabicName = s.SubFeatureArabicName,
                                    ActionName = s.ActionName,
                                    ControllerName = s.ControllerName,
                                    AreaName = s.AreaName,
                                    IsCheckerRequired = s.IsCheckerRequired, RateCardRoleId = s.RateCardRoleId, 
                                    UserRole=s.UserRole, ParentCustomerId = s.ParentCustomerId
                                }).ToList()
                            });                            
                        }
                    });
                }
                return menuItems;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: buildUserMenu :: userid:- {0} & Error:: {1}", userId, ex.ToString());
            }
            return menuItems;
        }

        public List<KeyValuePair<string, List<MenuItem>>> BuildUserMenuKeyPairs(List<UserMenuItems> source, bool IsSuperAdmin,
             List<string> sCollection)
        {
            var Mitems = new List<KeyValuePair<string, List<MenuItem>>>();
            try
            {
                var outParam = new List<MenuItem>();
                var makers = BuildMenuByPageType(source, "Maker", IsSuperAdmin, sCollection, out outParam);
                var checkers = BuildMenuByPageType(source, "Checker", IsSuperAdmin, sCollection, out outParam);
                var reports = BuildMenuByPageType(source, "Reports", IsSuperAdmin, sCollection, out outParam);
                if (makers.Value.Any())
                {
                    Mitems.Add(makers);
                }
                if (checkers.Value.Any())
                {
                    Mitems.Add(checkers);
                }
                if (reports.Value.Any())
                {
                    Mitems.Add(new KeyValuePair<string, List<MenuItem>>("", outParam));
                }
                return Mitems;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: BuildUserMenuKeyPairs :: {0}", ex.ToString());
            }
            return Mitems;
        }


        public KeyValuePair<string, List<MenuItem>> BuildMenuByPageType(List<UserMenuItems> source, string pageType, bool IsSuperAdmin,
          List<string> sCollection, out List<MenuItem> _menuitems)
        {
            var keyvaluemenuItems = new KeyValuePair<string, List<MenuItem>>();
            try
            {
                var menuItems = new List<MenuItem>();
                var checkerItems = source.Where(w => w.PageType.Equals(pageType)).ToList();
                var makerFeatureIds = checkerItems.Select(s => s.FeatureId).Distinct().ToList();
                makerFeatureIds.ForEach(x =>
                {
                    var featureItems = checkerItems.Where(w => w.FeatureId == x);
                    var userMenuItemses = featureItems as UserMenuItems[] ?? featureItems.ToArray();
                    if (userMenuItemses.Any())
                    {
                        var feature = userMenuItemses.FirstOrDefault();
                        menuItems.Add(new MenuItem
                        {
                            Name = feature.MenuName,
                            ArabicName = feature.ArabicName,
                            MenuIcon = feature.MenuIcon,
                            ChildMenuItems = featureItems.Select(s => new SubMenuItems
                            {
                                Name = s.SubFeature,
                                ArabicName = s.SubFeatureArabicName,
                                ActionName = s.ActionName,
                                ControllerName = s.ControllerName,
                                AreaName = s.AreaName,
                                IsCheckerRequired = s.IsCheckerRequired,UserRole = s.UserRole, 
                                RateCardRoleId = s.RateCardRoleId, ParentCustomerId = s.ParentCustomerId
                            }).ToList()
                        });
                    }
                });
                _menuitems = menuItems;
                if (!IsSuperAdmin)
                {
                    if (sCollection.Any())
                    {
                        foreach (var pg in sCollection)
                        {
                            foreach (var ParentPlugin in menuItems)
                            {

                                var item =
                                    ParentPlugin.ChildMenuItems.FirstOrDefault(
                                        w => w.Name.Equals(pg, StringComparison.OrdinalIgnoreCase));
                                if (item != null)
                                {
                                    ParentPlugin.ChildMenuItems.Remove(item);
                                }
                            }
                        }
                    }
                }
                keyvaluemenuItems = new KeyValuePair<string, List<MenuItem>>(pageType, menuItems);
                return keyvaluemenuItems;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error throuing :: Build Menu By PageType :: pageType :- {0} & Error :- {1}", pageType, ex.ToString());
            }
            _menuitems = keyvaluemenuItems.Value;
            return keyvaluemenuItems;
        }
    }
}